﻿/*
 * Globalize Culture de-DE
 *
 * http://github.com/jquery/globalize
 *
 * Copyright Software Freedom Conservancy, Inc.
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 *
 * This file was generated by the Globalize Culture Generator
 * Translation: bugs found in this file need to be fixed in the generator
 */

(function (window, undefined) {

    var Globalize;

    if (typeof require !== "undefined" &&
        typeof exports !== "undefined" &&
        typeof module !== "undefined") {
        // Assume CommonJS
        Globalize = require("globalize");
    } else {
        // Global variable
        Globalize = window.Globalize;
    }

    Globalize.addCultureInfo("de-DE", "default", {
        name: "de-DE",
        englishName: "German (Germany)",
        nativeName: "Deutsch (Deutschland)",
        language: "de",
        numberFormat: {
            ",": ".",
            ".": ",",
            "NaN": "n. def.",
            negativeInfinity: "-unendlich",
            positiveInfinity: "+unendlich",
            percent: {
                pattern: ["-n%", "n%"],
                ",": ".",
                ".": ","
            },
            currency: {
                pattern: ["-n $", "n $"],
                ",": ".",
                ".": ",",
                symbol: "€"
            }
        },
        calendars: {
            standard: {
                "/": ".",
                firstDay: 1,
                days: {
                    names: ["Sonntag", "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag"],
                    namesAbbr: ["So", "Mo", "Di", "Mi", "Do", "Fr", "Sa"],
                    namesShort: ["So", "Mo", "Di", "Mi", "Do", "Fr", "Sa"]
                },
                months: {
                    names: ["Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember", ""],
                    namesAbbr: ["Jan", "Feb", "Mrz", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dez", ""]
                },
                AM: null,
                PM: null,
                eras: [{ "name": "n. Chr.", "start": null, "offset": 0 }],
                patterns: {
                    d: "dd.MM.yyyy",
                    D: "dddd, d. MMMM yyyy",
                    t: "HH:mm",
                    T: "HH:mm:ss",
                    f: "dddd, d. MMMM yyyy HH:mm",
                    F: "dddd, d. MMMM yyyy HH:mm:ss",
                    M: "dd MMMM",
                    Y: "MMMM yyyy"
                }
            }
        },
        messages: {
            "Connection": "Verbindung",
            "Defaults": "Standard Werte",
            "Login": "Anmelden",
            "File": "Datei",
            "Exit": "Beenden",
            "Help": "Hilfe",
            "About": "Über",
            "Host": "Rechner",
            "Database": "Datenbank",
            "User": "Benutzer",
            "EnterUser": "Answendungs-Benutzer eingeben",
            "Password": "Kennwort",
            "EnterPassword": "Anwendungs-Kennwort eingeben",
            "Language": "Sprache",
            "SelectLanguage": "Anwendungs-Sprache auswählen",
            "Role": "Rolle",
            "Client": "Mandant",
            "Organization": "Organisation",
            "Date": "Datum",
            "Warehouse": "Lager",
            "Printer": "Drucker",
            "Connected": "Verbunden",
            "NotConnected": "Nicht verbunden",
            "DatabaseNotFound": "Datenbank nicht gefunden",
            "UserPwdError": "Benutzer und Kennwort stimmen nicht überein",
            "RoleNotFound": "Rolle nicht gefunden/komplett",
            "Authorized": "Authorisiert",
            "Ok": "Ok",
            "Cancel": "Abbruch",
            "VersionConflict": "Versions Konflikt:",
            "VersionInfo": "Server <> Arbeitsstation",
            "PleaseUpgrade": "Bitte das Aktualisierung-Programm (update) starten",
            "GoodMorning": "Guten Morgen",
            "GoodAfternoon": "Guten Tag",
            "GoodEvening": "Guten Abend",
            
            //New Resource

            "Back": "zurück",
            "SelectRole": "Wählen Sie Ihr Rolle",
            "SelectOrg": "Wählen Sie die Organisation",
            "SelectClient": "Wählen Sie den Mandanten",
            "SelectWarehouse": "Wählen Sie das Lager",
            "VerifyUserLanguage": "Überprüfen Benutzer und Sprache",
            "LoadingPreference": "Ihre Einstellungen werden geladen",
            "Completed": "Sie werden angemeldet",
            //new
            "RememberMe": "Merken",
            "FillMandatoryFields": "Pflichtfelder ausfüllen",
            "BothPwdNotMatch": "Beide Passwörter müssen übereinstimmen.",
            "mustMatchCriteria": "Die Mindestlänge für das Passwort beträgt 5. Das Passwort muss mindestens 1 Großbuchstabe, 1 Kleinbuchstabe, ein Sonderzeichen (@ $!% *? &) Und eine Ziffer enthalten. Das Passwort muss mit dem Zeichen beginnen.",
            "NotLoginUser": "Benutzer kann sich nicht beim System anmelden",
            "MaxFailedLoginAttempts": "Benutzerkonto ist gesperrt. Die maximale Anzahl fehlgeschlagener Anmeldeversuche überschreitet das definierte Limit. Bitte wenden Sie sich an den Administrator.",
            "UserNotFound": "Benutzername ist falsch.",
            "RoleNotDefined": "Für diesen Benutzer ist keine Rolle definiert",
            "oldNewSamePwd": "Altes und neues Passwort müssen unterschiedlich sein.",
            "NewPassword": "Neues VA-Passwort",
            "NewCPassword": "Bestätigen Sie das neue VA-Passwort",
            "EnterOTP": "Geben Sie OTP ein",
            "WrongOTP": "Falsches OTP eingegeben",
            "ScanQRCode": "Scannen Sie den Code mit Google Authenticator",
            "EnterVerCode": "Geben Sie das von Ihrer mobilen Anwendung generierte OTP ein",
            "PwdExpired": "Benutzerkennwort abgelaufen",
            "ActDisabled": "Account wurde deaktiviert",
            "ActExpired": "Konto ist abgelaufen",
            "AdminUserNotFound": "Der Administrator-Benutzername ist falsch.",
            "AdminUserPwdError": "Der Administrator stimmt nicht mit dem Kennwort überein",
            "AdminPwdExpired": "Kennwort des Administrators abgelaufen",
            "AdminActDisabled": "Das Administratorkonto wurde deaktiviert",
            "AdminActExpired": "Das Administratorkonto ist abgelaufen",
            "AdminMaxFailedLoginAttempts": "Admin-Benutzerkonto ist gesperrt. Die maximale Anzahl fehlgeschlagener Anmeldeversuche überschreitet das definierte Limit. Bitte wenden Sie sich an den Administrator.",
        }


    });

}(this));
