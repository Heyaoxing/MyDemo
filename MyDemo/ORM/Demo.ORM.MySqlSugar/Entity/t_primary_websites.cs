using System;
namespace Models
{
    public class t_primary_websites{
                        
    public Int64 ID {get;set;}

    public Int64 Source_ID {get;set;}

    public string WebSite_Url {get;set;}

    public int Level {get;set;}

    public int Status {get;set;}

    public DateTime create_time {get;set;}

    public SByte is_erased {get;set;}

   }
            
}