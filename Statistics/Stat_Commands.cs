﻿using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Terraria;
using TerrariaApi;
using TerrariaApi.Server;

using TShockAPI;
using TShockAPI.DB;

namespace Statistics
{
    public class sCommands
    {
        /* Yama's suggestions */
        public static void UI_Extended(CommandArgs args)
        {
            if (args.Parameters[0] == "self")
            {
                sPlayer player = sTools.GetPlayer(args.Player.Index);

                if (player != null)
                {
                    int pageNumber;
                    if (!PaginationTools.TryParsePageNumber(args.Parameters, 1, args.Player, out pageNumber))
                        return;
                    else
                    {
                        var uixInfo = new List<string>();
                        var time_1 = DateTime.Now.Subtract(DateTime.Parse(player.firstLogin));

                        uixInfo.Add(string.Format("UIX info for {0}", args.Parameters[0]));

                        uixInfo.Add(string.Format("First login: {0} ({1} ago)",
                            player.firstLogin, sTools.timeSpanPlayed(time_1)));

                        uixInfo.Add("Last seen: Now");

                        uixInfo.Add(string.Format("Logged in {0} times since registering", player.loginCount));
                        uixInfo.Add(string.Format("Known accounts: {0}", string.Join(", ", player.knownAccounts.Split(','))));
                        uixInfo.Add(string.Format("Known IPs: {0}", string.Join(", ", player.knownIPs.Split(','))));

                        PaginationTools.SendPage(args.Player, pageNumber, uixInfo, new PaginationTools.Settings 
                        {
                            HeaderFormat = "Extended User Information [Page {0} of {1}]",
                            HeaderTextColor = Color.Lime,
                            LineTextColor = Color.White,
                            FooterFormat = string.Format("/uix {0} {1} for more", args.Parameters[0], pageNumber + 1),
                            FooterTextColor = Color.Lime                            
                        });
                    }
                }
                else
                {
                    args.Player.SendErrorMessage("Something broke. Please try again later");
                }
            }
            else
            {
                sPlayer player = sTools.GetPlayer(args.Parameters[0]);

                int pageNumber;
                if (!PaginationTools.TryParsePageNumber(args.Parameters, 1, args.Player, out pageNumber))
                    return;

                if (player != null)
                {
                    var uixInfo = new List<string>();
                    var time_1 = DateTime.Now.Subtract(DateTime.Parse(player.firstLogin));

                    uixInfo.Add(string.Format("UIX info for {0}", args.Parameters[0]));

                    uixInfo.Add(string.Format("First login: {0} ({1} ago)",
                        player.firstLogin, sTools.timeSpanPlayed(time_1)));

                    uixInfo.Add("Last seen: Now");

                    uixInfo.Add(string.Format("Logged in {0} times since registering", player.loginCount));
                    uixInfo.Add(string.Format("Known accounts: {0}", string.Join(", ", player.knownAccounts.Split(','))));
                    uixInfo.Add(string.Format("Known IPs: {0}", string.Join(", ", player.knownIPs.Split(','))));

                    PaginationTools.SendPage(args.Player, pageNumber, uixInfo, new PaginationTools.Settings
                    {
                        HeaderFormat = "Extended User Information [Page {0} of {1}]",
                        HeaderTextColor = Color.Lime,
                        LineTextColor = Color.White,
                        FooterFormat = string.Format("/uix {0} {1} for more", args.Parameters[0], pageNumber + 1),
                        FooterTextColor = Color.Lime
                    });
                }
                else
                {
                    storedPlayer storedplayer = sTools.GetstoredPlayer(args.Parameters[0]);

                    if (storedplayer != null)
                    {
                        var uixInfo = new List<string>();
                        var time_1 = DateTime.Now.Subtract(DateTime.Parse(storedplayer.firstLogin));

                        uixInfo.Add(string.Format("UIX info for {0}", args.Parameters[0]));

                        uixInfo.Add(string.Format("First login: {0} ({1} ago)",
                            storedplayer.firstLogin, sTools.timeSpanPlayed(time_1)));

                        uixInfo.Add(string.Format("Last seen: {0}. Overall play time: {1}", storedplayer.lastSeen,
                            sTools.timePlayed(storedplayer.totalTime)));

                        uixInfo.Add(string.Format("Logged in {0} times since registering", storedplayer.loginCount));
                        uixInfo.Add(string.Format("Known accounts: {0}", string.Join(", ", storedplayer.knownAccounts.Split(','))));
                        uixInfo.Add(string.Format("Known IPs: {0}", string.Join(", ", storedplayer.knownIPs.Split(','))));

                        PaginationTools.SendPage(args.Player, pageNumber, uixInfo, new PaginationTools.Settings
                        {
                            HeaderFormat = "Extended User Information [Page {0} of {1}]",
                            HeaderTextColor = Color.Lime,
                            LineTextColor = Color.White,
                            FooterFormat = string.Format("/uix {0} {1} for more", args.Parameters[0], pageNumber + 1),
                            FooterTextColor = Color.Lime
                        });
                    }
                    else
                        args.Player.SendErrorMessage("Invalid player. Try /check name {0} to make sure you're using the right username",
                        args.Parameters[0]);
                }
            }
        }

        public static void UI_Character(CommandArgs args)
        {
            if (args.Parameters[0] == "self")
            {
                sPlayer player = sTools.GetPlayer(args.Player.Index);

                if (player != null)
                {
                    int pageNumber;
                    if (!PaginationTools.TryParsePageNumber(args.Parameters, 1, args.Player, out pageNumber))
                        return;
                    else
                    {
                        var uicInfo = new List<string>();
                        var time_1 = DateTime.Now.Subtract(DateTime.Parse(player.firstLogin));

                        uicInfo.Add(string.Format("Character info for {0}", args.Parameters[0]));

                        uicInfo.Add(string.Format("First login: {0} ({1} ago)",
                            player.firstLogin, sTools.timeSpanPlayed(time_1)));

                        uicInfo.Add("Last seen: Now");

                        uicInfo.Add(string.Format("Logged in {0} times since registering.  Overall play time: {1}",
                            player.loginCount, sTools.timePlayed(player.TimePlayed)));

                        PaginationTools.SendPage(args.Player, pageNumber, uicInfo, new PaginationTools.Settings
                        {
                            HeaderFormat = "Character Information [Page {0} of {1}]",
                            HeaderTextColor = Color.Lime,
                            LineTextColor = Color.White,
                            FooterFormat = string.Format("/uic {0} {1} for more", args.Parameters[0], pageNumber + 1),
                            FooterTextColor = Color.Lime
                        });
                    }
                }
                else
                {
                    args.Player.SendErrorMessage("Something broke. Please try again later");
                }
            }
            else
            {
                sPlayer player = sTools.GetPlayer(args.Parameters[0]);

                int pageNumber;
                if (!PaginationTools.TryParsePageNumber(args.Parameters, 1, args.Player, out pageNumber))
                    return;

                if (player != null)
                {

                    var uicInfo = new List<string>();
                    var time_1 = DateTime.Now.Subtract(DateTime.Parse(player.firstLogin));

                    uicInfo.Add(string.Format("Character info for {0}", args.Parameters[0]));

                    uicInfo.Add(string.Format("First login: {0} ({1} ago)",
                        player.firstLogin, sTools.timeSpanPlayed(time_1)));

                    uicInfo.Add("Last seen: Now");

                    uicInfo.Add(string.Format("Logged in {0} times since registering.  Overall play time: {1}",
                            player.loginCount, sTools.timePlayed(player.TimePlayed)));

                    PaginationTools.SendPage(args.Player, pageNumber, uicInfo, new PaginationTools.Settings
                    {
                        HeaderFormat = "Extended User Information [Page {0} of {1}]",
                        HeaderTextColor = Color.Lime,
                        LineTextColor = Color.White,
                        FooterFormat = string.Format("/uic {0} {1} for more", args.Parameters[0], pageNumber + 1),
                        FooterTextColor = Color.Lime
                    });
                }
                else
                {
                    storedPlayer storedplayer = sTools.GetstoredPlayer(args.Parameters[0]);

                    if (storedplayer != null)
                    {
                        var uicInfo = new List<string>();
                        var time_1 = DateTime.Now.Subtract(DateTime.Parse(storedplayer.firstLogin));
                        var time_2 = DateTime.Now.Subtract(DateTime.Parse(storedplayer.lastSeen));

                        uicInfo.Add(string.Format("Character info for {0}", args.Parameters[0]));

                        uicInfo.Add(string.Format("First login: {0} ({1} ago)",
                            storedplayer.firstLogin, sTools.timeSpanPlayed(time_1)));

                        uicInfo.Add(string.Format("Last seen: {0} ({1} ago).", storedplayer.lastSeen,
                            sTools.timeSpanPlayed(time_2)));

                        uicInfo.Add(string.Format("Logged in {0} times since registering.  Overall play time: {1}",
                            storedplayer.loginCount, sTools.timePlayed(storedplayer.totalTime)));

                        PaginationTools.SendPage(args.Player, pageNumber, uicInfo, new PaginationTools.Settings
                        {
                            HeaderFormat = "Character Information [Page {0} of {1}]",
                            HeaderTextColor = Color.Lime,
                            LineTextColor = Color.White,
                            FooterFormat = string.Format("/uic {0} {1} for more", args.Parameters[0], pageNumber + 1),
                            FooterTextColor = Color.Lime
                        });
                    }
                    else
                        args.Player.SendErrorMessage("Invalid player. Try /check name {0} to make sure you're using the right username",
                        args.Parameters[0]);
                }
            }
        }
        /* ------------------ */


        public static void stat_Check(CommandArgs args)
        {
            sTools.handler.RunSubcommand(args);
        }

        public static void check_Time(CommandArgs args)
        {
            if (args.Parameters.Count > 0)
                if (args.Parameters[0] == "self")
                {
                    var player = sTools.GetPlayer(args.Player.Index);

                    if (player != null)
                    {
                        args.Player.SendInfoMessage("You have played for {0}", sTools.timePlayed(player.TimePlayed));
                    }
                    else
                        args.Player.SendErrorMessage("Something broke. Please try again later");
                }
                else
                {
                    var player = sTools.GetPlayer(args.Parameters[0]);

                    if (player != null)
                    {
                        args.Player.SendInfoMessage("{0} has played for {1}", player.TSPlayer.UserAccountName, 
                            sTools.timePlayed(player.TimePlayed));
                    }
                    else
                        args.Player.SendErrorMessage("Invalid player. Try /check name {0} to make sure you're using the right username",
                        args.Parameters[0]);
                }
        }

        public static void check_Name(CommandArgs args)
        {
            var player = TShock.Utils.FindPlayer(args.Parameters[0]);

            if (player.Count > 1)
                TShock.Utils.SendMultipleMatchError(args.Player, player.Select(ply => ply.Name));
            else if (player.Count == 1)
                args.Player.SendInfoMessage("User name of {0} is {1}", player[0].Name, player[0].UserAccountName);
            else
                args.Player.SendErrorMessage("No players matched your query '{0}'", args.Parameters[0]);
        }

        public static void check_Kills(CommandArgs args)
        {
            if (args.Parameters[0] == "self")
            {
                var player = sTools.GetPlayer(args.Player.Index);

                if (player != null)
                    args.Player.SendInfoMessage("You have killed {0} player{4}, {1} mob{5}, {2} boss{6} and died {3} time{7}",
                        player.kills, player.mobkills, player.bosskills, player.deaths,
                        player.kills > 1 || player.kills == 0 ? "s" : "", player.mobkills > 1 || player.mobkills == 0 ? "s" : "",
                        player.bosskills > 1 || player.bosskills == 0 ? "es" : "", player.deaths > 1 || player.deaths == 0 ? "s" : "");
                else
                    args.Player.SendErrorMessage("Something broke. Please try again later");
            }
            else
            {
                var player = sTools.GetPlayer(args.Parameters[0]);

                if (player != null)
                    args.Player.SendInfoMessage("{0} has killed {1} player{5}, {2} mob{6}, {3} boss{7} and died {4} time{8}",
                        player.TSPlayer.UserAccountName, player.kills, player.mobkills, player.bosskills, player.deaths,
                        player.kills > 1 || player.kills == 0 ? "s" : "", player.mobkills > 1 || player.mobkills == 0 ? "s" : "",
                        player.bosskills > 1 || player.bosskills == 0 ? "es" : "", player.deaths > 1 || player.deaths == 0 ? "s" : "");
                else
                    args.Player.SendErrorMessage("Invalid player. Try /check name {0} to make sure you're using the right username",
                        args.Parameters[0]);
            }
        }

        public static void check_Afk(CommandArgs args)
        {
            if (args.Parameters[0] == "self")
            {
                var player = sTools.GetPlayer(args.Player.Index);

                if (player != null)
                    if (player.AFK)
                        args.Player.SendInfoMessage("You have been away for {0} seconds", player.AFKcount);
                    else
                        args.Player.SendInfoMessage("You are not away");
                else
                    args.Player.SendErrorMessage("Something broke. Please try again later");
            }
            else
            {
                var player = sTools.GetPlayer(args.Parameters[0]);

                if (player != null)
                    if (player.AFK)
                        args.Player.SendInfoMessage("{0} has been away for {1} second{0}",
                            player.TSPlayer.UserAccountName, player.AFKcount,
                            player.AFKcount > 1 || player.AFKcount == 0 ? "s" : "");
                    else
                        args.Player.SendInfoMessage("{0} is not away", player.TSPlayer.UserAccountName);
                else
                    args.Player.SendErrorMessage("Invalid player. Try /check name {0} to make sure you're using the right username",
                        args.Parameters[0]);
            }
        }
    }
}