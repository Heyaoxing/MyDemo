using System;
namespace Models
{
    public class t_exclude_websites{
                        
    public Int64 ID {get;set;}

    public string WebSite_Name {get;set;}

    public string WebSite_Url {get;set;}

    public DateTime create_time {get;set;}

    public SByte is_erased {get;set;}

   }
            
}