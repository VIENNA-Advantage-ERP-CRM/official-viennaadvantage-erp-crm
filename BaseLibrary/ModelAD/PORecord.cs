﻿/********************************************************
 * Module Name    : PO Delete
 * Purpose        : To delete various attachments/chats/etc. of a record
 * Class Used     : ---
 * Chronological Development
 * Veena Pandey     -Feb-2009
 ******************************************************/

using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using VAdvantage.DataBase;
using VAdvantage.Logging;
using System.Collections.Generic;
using System.Linq;

namespace VAdvantage.Model
{
    /// <summary>
    /// To delete various attachments/chats/etc. of a record
    /// </summary>
   public  class PORecord
    {

        private static VLogger log = VLogger.GetVLogger(typeof(PORecord).FullName);

        ////	Parent Table ID
        //private static int[] _parents = new int[]{
        ////X_C_Order.Table_ID,
        ////X_CM_Container.Table_ID
        //};

        private static Dictionary<int,string> _parents = 
            new Dictionary<int, string>();

        public static void AddParent(int table_id, string table_name)
        {
            if (!_parents.ContainsKey(table_id))
                _parents.Add(table_id, table_name);
        }
        ////	Parent Table Names
        //private static string[] _parentNames = new string[]{
        ////X_C_Order.Table_Name,
        ////X_CM_Container.Table_Name
        //};
        // Child Table ID
        private static Dictionary<int, string> _parentChilds =
            new Dictionary<int, string>();

        public static void AddParentChild(int table_id, string table_name)
        {
            if (!_parentChilds.ContainsKey(table_id))
                _parentChilds.Add(table_id, table_name);
        }

        // Child Table ID

        //X_C_OrderLine.Table_ID,
        //X_CM_Container_Element.Table_ID
        //};

        // Child Table Names
        // private static string[] _parentChildNames = new string[]{
        //X_C_OrderLine.Table_Name,
        //X_CM_Container_Element.Table_Name
        //};

        private static Dictionary<int, string> _cascades =
       new Dictionary<int, string>();

        public static void AddCascade(int table_id, string table_name)
        {
            if (!_cascades.ContainsKey(table_id))
                _cascades.Add(table_id, table_name);
        }

        //	Cascade Table ID
        //private static int[] _cascades = new int[]{
        //X_VAF_Attachment.Table_ID,
        //X_VAF_Archive.Table_ID,
        //X_VAF_Notice.Table_ID,
        //X_MailAttachment1.Table_ID,
        //X_AppointmentsInfo.Table_ID,
        // X_K_Index.Table_ID,

        //};

        //	Cascade Table Names
        //private static string[] _cascadeNames = new string[]{
        //X_VAF_Attachment.Table_Name,
        //X_VAF_Archive.Table_Name,
        //X_VAF_Notice.Table_Name,
        //X_MailAttachment1.Table_Name,
        //X_AppointmentsInfo.Table_Name,
        //X_K_Index.Table_Name 
      ///  };

private static Dictionary<int, string> _restricts =
      new Dictionary<int, string>();

public static void AddRestricts(int table_id, string table_name)
{
    if (!_restricts.ContainsKey(table_id))
        _restricts.Add(table_id, table_name);
}

//	Restrict Table ID
//private static int[] _restricts = new int[]{
//        //X_CM_Chat.Table_ID,
//        //X_R_Request.Table_ID
//        };

        //	Restrict Table Names
        //private static string[] _restrictNames = new string[]{
        ////X_CM_Chat.Table_Name,
        ////    X_R_Request.Table_Name
        //};

        /// <summary>
        /// Delete Cascade including (selected)parent relationships
        /// </summary>
        /// <param name="VAF_TableView_ID">table id</param>
        /// <param name="Record_ID">record id</param>
        /// <param name="trxName">transaction object</param>
        /// <returns>false if could not be deleted</returns>
        /// <author>Veena Pandey</author>
        public static bool DeleteCascade(int VAF_TableView_ID, int Record_ID, Trx trxName)
        {
            bool retval = true;
            //	Table Loop
            foreach ( var item in _cascades)
            {
                //	DELETE FROM table WHERE VAF_TableView_ID=#1 AND Record_ID=#2
                if (/*_cascades*/ item.Key != VAF_TableView_ID)
                {
                    StringBuilder sql = new StringBuilder("DELETE FROM ")
                        .Append(item.Value)
                        .Append(" WHERE VAF_TableView_ID=" + VAF_TableView_ID + " AND Record_ID=" + Record_ID);
                    int no = DataBase.DB.ExecuteQuery(sql.ToString(), null, trxName);
                    if (no > 0)
                    {
                        log.Config(item.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                    }
                    else if (no < 0)
                    {
                        log.Severe(item.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                        retval = false;
                        break;
                        //return false;
                    }
                }
            }
            //	Parent Loop
            //for (int j = 0; j < _parents.Length; j++)
            int j = -1;
            foreach(var prnts in _parents)
            {
                j++;
                // DELETE FROM VAF_Attachment WHERE VAF_TableView_ID=1 AND Record_ID IN 
                //	(SELECT VAB_InvoiceLine_ID FROM VAB_InvoiceLine WHERE VAB_Invoice_ID=1)
                if (prnts.Key == VAF_TableView_ID)
                {
                    var pcitm = _parentChilds.ElementAt(j);
                    int VAF_TableView_IDchild = pcitm.Key; ;//. prnts _parentChilds[j];

                    //Object[] param = new Object[]{new Integer(VAF_TableView_IDchild), new Integer(Record_ID)};
                    //for (int i = 0; i < _cascades.Length; i++)
                        foreach (var  casItem in  _cascades)
                        {
                        StringBuilder sql = new StringBuilder("DELETE FROM ")
                            .Append(casItem.Value)
                            .Append(" WHERE VAF_TableView_ID=" + VAF_TableView_IDchild + " AND Record_ID IN (SELECT ")
                            .Append(pcitm.Value).Append("_ID FROM ")
                            .Append(pcitm.Value).Append(" WHERE ")
                            .Append(prnts.Value).Append("_ID=" + Record_ID + ")");
                        int no = DataBase.DB.ExecuteQuery(sql.ToString(), null, trxName);
                        if (no > 0)
                        {
                            log.Config(casItem.Value + " " + prnts.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                        }
                        else if (no < 0)
                        {
                            log.Severe(casItem.Value + " " + prnts.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                            retval = false;
                            break;
                            //return false;
                        }
                    }
                }
            }
            return retval;
        }


        /// <summary>
        /// Delete Cascade including (selected)parent relationships
        /// </summary>
        /// <param name="VAF_TableView_ID">table id</param>
        /// <param name="Record_ID">record id</param>
        /// <param name="trxName">transaction object</param>
        /// <returns>list of log</returns>
        public static System.Collections.Generic.List<String> DeletePOCascade(int VAF_TableView_ID, int Record_ID, Trx trx)
        {
            //	Table Loop

            System.Collections.Generic.List<String> retList = new System.Collections.Generic.List<string>();

            foreach (var cItem in _cascades)
                //for (int i = 0; i < _cascades.Length; i++)
            {
                //	DELETE FROM table WHERE VAF_TableView_ID=#1 AND Record_ID=#2
                if (cItem.Key != VAF_TableView_ID)
                {
                    StringBuilder sql = new StringBuilder("DELETE FROM ")
                        .Append(cItem.Value)
                        .Append(" WHERE VAF_TableView_ID=" + VAF_TableView_ID + " AND Record_ID=" + Record_ID);
                    int no = DataBase.DB.ExecuteQuery(sql.ToString(), null, trx);
                    if (no > 0)
                    {
                        retList.Add(cItem.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                        log.Config(cItem.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                    }
                    else if (no < 0)
                    {
                        retList.Add(cItem.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                        log.Severe(cItem.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                        break;
                        //return retList;
                    }
                }
            }
            //	Parent Loop
            int j = -1;
            foreach (var prnts in _parents)
            {
                j++;
                // DELETE FROM VAF_Attachment WHERE VAF_TableView_ID=1 AND Record_ID IN 
                //	(SELECT VAB_InvoiceLine_ID FROM VAB_InvoiceLine WHERE VAB_Invoice_ID=1)
                if (prnts.Key == VAF_TableView_ID)
                {
                    var prntChild = _parentChilds.ElementAt(j);
                    int VAF_TableView_IDchild = prntChild.Key;
                    //Object[] param = new Object[]{new Integer(VAF_TableView_IDchild), new Integer(Record_ID)};
                    //for (int i = 0; i < _cascades.Length; i++)
                        foreach(var casItem in  _cascades)
                        {
                        StringBuilder sql = new StringBuilder("DELETE FROM ")
                            .Append(casItem.Value)
                            .Append(" WHERE VAF_TableView_ID=" + VAF_TableView_IDchild + " AND Record_ID IN (SELECT ")
                            .Append(prntChild.Value).Append("_ID FROM ")
                            .Append(prntChild.Value).Append(" WHERE ")
                            .Append(prnts.Value).Append("_ID=" + Record_ID + ")");
                        int no = DataBase.DB.ExecuteQuery(sql.ToString(), null, trx);
                        if (no > 0)
                        {
                            retList.Add(casItem.Value + " " + prnts.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                            log.Config(casItem.Value + " " + prnts.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                        }
                        else if (no < 0)
                        {
                            retList.Add(casItem.Value + " " + prnts.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                            log.Severe(casItem.Value + " " + prnts.Value + " (" + VAF_TableView_ID + "/" + Record_ID + ") #" + no);
                            break;
                           // return retList;
                        }
                    }
                }
            }
            return retList;
        }


        /// <summary>
        /// An entry Exists for restrict table/record combination
        /// </summary>
        /// <param name="VAF_TableView_ID">table id</param>
        /// <param name="Record_ID">record id</param>
        /// <param name="trxName">transaction</param>
        /// <returns>error message (Table Name) or null</returns>
        /// <author>Veena Pandey</author>
        public static string Exists(int VAF_TableView_ID, int Record_ID, Trx trx)
        {
            //	Table Loop only
            //for (int i = 0; i < _restricts.Length; i++)
            foreach(var item in _restricts)
            {
                //	SELECT COUNT(*) FROM table WHERE VAF_TableView_ID=#1 AND Record_ID=#2
                StringBuilder sql = new StringBuilder("SELECT COUNT(*) FROM ")
                    .Append(item.Value)
                    .Append(" WHERE VAF_TableView_ID=" + VAF_TableView_ID + " AND Record_ID=" + Record_ID);
                int no = DataBase.DB.GetSQLValue(trx,sql.ToString());
                if (no > 0)
                    return item.Value;// _restrictNames[i];
            }
            return null;
        }
    }
}
