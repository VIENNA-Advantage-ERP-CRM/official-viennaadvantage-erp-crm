﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using VAdvantage.Utility;
using VIS.DBase;
using VIS.Models;

namespace VIS.Helpers
{
    public class ShortcutHelper
    {

        public static List<ShortcutItemModel> GetShortcutItems(Ctx ctx)
        {

            List<ShortcutItemModel> lst = new List<ShortcutItemModel>();


            bool isBaseLang = Env.IsBaseLanguage(ctx, "VAF_Shortcut");


            int VAF_Client_ID = ctx.GetVAF_Client_ID();
            StringBuilder sql = new StringBuilder(@" SELECT o.Name AS Name, 
                                    o.VAF_Image_ID AS VAF_Image_ID,
                                  o.Classname AS ClassName  ,
                                   o.VAF_Shortcut_ID,
                                  o.Action  AS Action   ,
                                  (
                                  CASE
                                    WHEN o.Action = 'W'
                                    THEN o.VAF_Screen_ID
                                    WHEN o.Action='P'
                                    OR o.Action  ='R'
                                    THEN o.VAF_Job_ID
                                    WHEN o.Action = 'B'
                                    THEN o.VAF_WorkBench_ID
                                    WHEN o.Action = 'T'
                                    THEN o.VAF_Task_ID
                                    WHEN o.Action = 'X'
                                    THEN o.VAF_Page_ID
                                    WHEN o.Action ='F'
                                    THEN o.VAF_Workflow_ID
                                    ELSE 0
                                  END ) AS ActionID,
                                
                                  (
                                  CASE
                                    WHEN
                                     (SELECT COUNT(*)
                                       FROM VAF_Shortcut i
                                      WHERE i.Parent_ID = o.VAF_Shortcut_ID and IsChild = 'Y') >0
                                    THEN 'Y'
                                    ELSE 'N'
                                  END) AS HasChild, o.VAF_Shortcut_ID as ID,  o.Url as Url,
                                  
                                  (SELECT COUNT(*) FROM VAF_ShortcutParameter WHERE VAF_Shortcut_ID=o.VAF_Shortcut_ID) as hasPara,");

            if (isBaseLang)
            {
                sql.Append(" COALESCE(o.DisplayName,o.Name) as Name2");
            }
            else
            {
                sql.Append(" COALESCE(trl.Name,o.Name) as Name2");
            }
            sql.Append(" FROM VAF_Shortcut o ");
            if (!isBaseLang)
            {
                sql.Append(" INNER JOIN VAF_Shortcut_TL trl ON o.VAF_Shortcut_ID = trl.VAF_Shortcut_ID AND trl.VAF_Language =")
                .Append("'").Append(Env.GetVAF_Language(ctx)).Append("'");
            }

            sql.Append(" WHERE o.VAF_Client_ID = 0 AND o.IsActive ='Y' AND o.IsChild = 'N' ");

            sql.Append(@"AND (o.VAF_Screen_ID IS NULL OR EXISTS (SELECT * FROM VAF_Screen_Rights w WHERE w.VAF_Screen_ID=o.VAF_Screen_ID AND w.IsReadWrite='Y' and VAF_Role_ID=" + ctx.GetVAF_Role_ID() + @"))
                        AND (o.VAF_Page_ID IS NULL OR EXISTS (SELECT * FROM VAF_Page_Rights f WHERE f.VAF_Page_id=o.VAF_Page_ID AND f.isreadwrite='Y' and VAF_Role_ID=" + ctx.GetVAF_Role_ID() + @"))
                        AND (o.VAF_Job_ID IS NULL OR EXISTS (SELECT * FROM VAF_Job_Rights p WHERE p.VAF_Job_id=o.VAF_Job_ID AND p.isreadwrite='Y' and VAF_Role_ID=" + ctx.GetVAF_Role_ID() + @"))");
            sql.Append("  ORDER BY SeqNo");

            IDataReader dr = null;
            try
            {
                dr = DB.ExecuteReader(sql.ToString(), null);
            }
            catch
            {
                if (dr != null)
                    dr.Close();
                return lst;
            }

            CreateShortcut(dr, lst, ctx);

            return lst;
        }

        private static void CreateShortcut(IDataReader dr, List<ShortcutItemModel> lst, Ctx ctx, bool isSetting = false)
        {
            while (dr.Read())
            {

                ShortcutItemModel itm = new ShortcutItemModel();

                itm.ShortcutName = Util.GetValueOfString(dr["Name2"]);
                itm.Action = Util.GetValueOfString(dr["Action"]);
                itm.ActionID = Util.GetValueOfInt(dr["ActionID"]);
                itm.SpecialAction = Util.GetValueOfString(dr["ClassName"]);
                itm.ActionName = Util.GetValueOfString(dr["Name"]);
                if (!isSetting)
                    itm.HasChild = "Y".Equals(Util.GetValueOfString(dr["HasChild"]));

                if (!string.IsNullOrEmpty(itm.SpecialAction))
                {
                    string className = itm.SpecialAction;
                    string prefix = "";
                    string nSpace = "";

                    try
                    {
                      //  Tuple<String, String> aInfo = null;
                        if (Env.GetModulePrefix(itm.ActionName, out prefix, out nSpace))
                        {
                            className = className.Replace(nSpace, prefix.Substring(0, prefix.Length - 1));

                        }
                        else
                        {
                            if (prefix.Length == 0)
                            {
                                prefix = "VIS_";
                            }
                            

                            nSpace = "VAdvantage";
                            if (className.Contains(nSpace))
                            {
                                className = className.Replace(nSpace, prefix.Substring(0, prefix.Length - 1));
                            }
                            nSpace = "ViennaAdvantage";
                            if (className.Contains(nSpace))
                            {
                                className = className.Replace(nSpace, prefix.Substring(0, prefix.Length - 1));
                            }
                        }
                    }
                    catch
                    {
                        // blank
                    }
                    itm.SpecialAction = className;
                }



                StringBuilder builder = new StringBuilder();

                if (Util.GetValueOfInt(dr["HASPARA"]) > 0)
                {
                    string strSql = "SELECT parametername, parametervalue,ISENCRYPTED FROM VAF_ShortcutParameter WHERE IsActive='Y' AND VAF_Shortcut_ID=" + Util.GetValueOfInt(dr["VAF_Shortcut_ID"]);
                    IDataReader drPara = null;
                    try
                    {
                        drPara = DB.ExecuteReader(strSql, null);
                        while (drPara.Read())
                        {
                            if (drPara["PARAMETERVALUE"] != null && drPara["PARAMETERVALUE"].ToString() != "")
                            {
                                string variableName = drPara["PARAMETERVALUE"].ToString();
                                String columnName = string.Empty;
                                string env = string.Empty;
                                if (variableName.Contains("@"))
                                {
                                    int index = variableName.IndexOf("@");
                                    columnName = variableName.Substring(index + 1);
                                    index = columnName.IndexOf("@");
                                    if (index == -1)
                                    {
                                        break;
                                    }
                                    columnName = columnName.Substring(0, index);
                                    env = ctx.GetContext(columnName);
                                }
                                else
                                {
                                    if (drPara["PARAMETERNAME"] != null && drPara["PARAMETERNAME"].ToString() != "")
                                    {
                                        columnName = drPara["PARAMETERNAME"].ToString();
                                    }
                                    env = variableName;
                                }

                                if (env.Length == 0)
                                {
                                    break;
                                }

                                if (drPara["ISENCRYPTED"].ToString().Equals("Y", StringComparison.OrdinalIgnoreCase))
                                {
                                    env = SecureEngine.Encrypt(env);
                                }
                                if (columnName.StartsWith("#"))
                                {
                                    while (columnName.StartsWith("#"))
                                    {
                                        columnName = columnName.Substring(1);
                                    }
                                }
                                builder.Append(columnName).Append("=").Append(env).Append('&');
                            }
                        }
                        builder.ToString().TrimEnd('&');
                        if (drPara != null)
                        {
                            drPara.Close();
                            drPara = null;
                        }
                    }
                    catch
                    {
                        if (drPara != null)
                        {
                            drPara.Close();
                            drPara = null;
                        }
                    
                    }
                }

                if ((builder.ToString().Length > 0))
                {
                    itm.Url = Util.GetValueOfString(dr["Url"]) + builder.ToString();
                }
                else
                {
                    itm.Url = Util.GetValueOfString(dr["Url"]);
                }

                itm.KeyID = Util.GetValueOfInt(dr["ID"]);
                int VAF_Image_ID = Util.GetValueOfInt(dr["VAF_Image_ID"]);
                if (VAF_Image_ID > 0)
                {
                    var img = new VAdvantage.Model.MVAFImage(ctx, VAF_Image_ID, null);

                    if (img.GetFontName() != null && img.GetFontName().Length > 0)
                    {
                        itm.HasImage = true;
                        itm.IsImageByteArray = false;
                        itm.IconUrl = img.GetFontName();
                    }
                    else if (img.GetImageURL() != null && img.GetImageURL().Length > 0)
                    {
                        itm.HasImage = true;
                        itm.IsImageByteArray = false;
                        itm.IconUrl = img.GetImageURL();
                    }
                    else if (img.GetBinaryData() != null)
                    {
                        itm.HasImage = true;
                        itm.IsImageByteArray = true;
                        itm.IconBytes = img.GetBinaryData();
                    }
                }
                lst.Add(itm);
            }
            dr.Close();
        }

        public static List<ShortcutItemModel> GetSettingItems(Ctx ctx, int VAF_Shortcut_ID)
        {

            List<ShortcutItemModel> lst = new List<ShortcutItemModel>();

            bool isBaseLang = Env.IsBaseLanguage(ctx, "VAF_Shortcut");


            string sql = @"SELECT o.Name AS Name,
                                  o.VAF_Image_ID AS VAF_Image_ID,
                                  o.Classname AS ClassName  ,
                                  o.Action  AS Action   ,
                                  (
                                  CASE
                                    WHEN o.action = 'W'
                                    THEN o.VAF_Screen_id
                                    WHEN o.action='P'
                                    OR o.action  ='R'
                                    THEN o.VAF_Job_id
                                    WHEN o.action = 'B'
                                    THEN o.VAF_workbench_id
                                    WHEN o.action = 'T'
                                    THEN o.VAF_Task_id
                                    WHEN o.action = 'X'
                                    THEN o.VAF_Page_id
                                    WHEN o.action ='F'
                                    THEN o.VAF_Workflow_id
                                    ELSE 0
                                  END ) AS ActionID, o.VAF_Shortcut_id as ID,  o.Url as Url, 
                                (SELECT COUNT(*) FROM VAF_ShortcutParameter WHERE VAF_Shortcut_ID=o.VAF_Shortcut_ID) as hasPara,";

            if (isBaseLang)
            {
                sql += " o.DisplayName as Name2 FROM VAF_Shortcut o ";
            }
            else
            {
                sql += " trl.Name as Name2 FROM VAF_Shortcut o INNER JOIN VAF_Shortcut_TL trl ON o.VAF_Shortcut_ID = trl.VAF_Shortcut_ID "
                         + " AND trl.VAF_Language =  '" + Env.GetVAF_Language(ctx) + "' ";
            }
            sql += @" WHERE o.VAF_Client_ID = 0
                                  AND o.IsActive         ='Y'
                                  AND o.IsChild          = 'Y'
                                 AND o.Parent_ID =  " + VAF_Shortcut_ID + @"
                        AND (o.VAF_Screen_ID IS NULL OR EXISTS (SELECT * FROM VAF_Screen_Rights w WHERE w.VAF_Screen_ID=o.VAF_Screen_ID AND w.IsReadWrite='Y' and VAF_Role_ID=" + ctx.GetVAF_Role_ID() + @"))
                        AND (o.VAF_Page_ID IS NULL OR EXISTS (SELECT * FROM VAF_Page_Rights f WHERE f.VAF_Page_ID=o.VAF_Page_ID AND f.IsReadWrite='Y' and VAF_Role_ID=" + ctx.GetVAF_Role_ID() + @"))
                        AND (o.VAF_Job_ID IS NULL OR EXISTS (SELECT * FROM VAF_Job_Rights p WHERE p.VAF_Job_ID=o.VAF_Job_ID AND p.IsReadWrite='Y' and VAF_Role_ID=" + ctx.GetVAF_Role_ID() + @"))
                        ORDER BY SeqNo";

            IDataReader dr = null;

            try
            {
                dr = DB.ExecuteReader(sql, null);
            }
            catch
            {
                if (dr != null)
                    dr.Close();
                return lst;
            }

            CreateShortcut(dr, lst, ctx,true);
            return lst;
        }
    }
}