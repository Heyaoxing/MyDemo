using System;
namespace Models
{
    public class t_filter_rule_configuration{
                        
    public Int64 ID {get;set;}

    public string Filter_KeyWord {get;set;}

    public int Filter_Type {get;set;}

    public int Filter_Position {get;set;}

    public DateTime create_time {get;set;}

    public SByte is_erased {get;set;}

   }
            
}