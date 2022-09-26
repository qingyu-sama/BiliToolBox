using BiliToolBox.Clients;
using System.Web;

BiliClient biliClient = new();
biliClient.Login(BiliToolBox.Enums.LoginType.QrCode);