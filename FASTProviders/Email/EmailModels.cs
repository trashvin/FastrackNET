using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class FastEmail
{
    public List<string> Receipients { get;set;}
    public List<string> CCList { get; set; }
    
    public string Subject { get; set; }
    public List<string> Attachments { get; set; }  //paths to file
    public string HTMLBody { get; set; }
}

[Serializable]
public class FastEmailConfiguration
{
    public string SenderAddress { get; set; }
    public string Password { get; set; }
    public string MailServer { get; set; }
    public int MailPort { get; set; }
    public bool EnableSSL { get; set; }

    public FastEmailConfiguration()
    {

    }

    public FastEmailConfiguration(string sender, string password, string server, int port , bool enableSSL)
    {
        SenderAddress = sender;
        Password = password;
        MailServer = server;
        MailPort = port;
        EnableSSL = enableSSL;
    }
}