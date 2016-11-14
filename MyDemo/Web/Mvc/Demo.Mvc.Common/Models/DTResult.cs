using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Mvc.Common.Models
{
    public class DTResult<T>
    {
        /// <summary>
        /// The draw counter that this object is a response to - from the draw parameter sent as part of the data request.
        /// Note that it is strongly recommended for security reasons that you cast this parameter to an integer, rather than simply echoing back to the client what it sent in the draw parameter, in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - not just the number of records being returned for this page of data).
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// The data to be displayed in the table.
        /// This is an array of data source objects, one for each row, which will be used by DataTables.
        /// Note that this parameter's name can be changed using the ajaxDT option's dataSrc property.
        /// </summary>
        public List<T> data { get; set; }
    }

    public abstract class DTRow
    {
        /// <summary>
        /// Set the ID property of the dt-tag tr node to this value
        /// </summary>
        public virtual string DT_RowId
        {
            get { return null; }
        }

        /// <summary>
        /// Add this class to the dt-tag tr node
        /// </summary>
        public virtual string DT_RowClass
        {
            get { return null; }
        }

        /// <summary>
        /// Add this data property to the row's dt-tag tr node allowing abstract data to be added to the node, using the HTML5 data-* attributes.
        /// This uses the jQuery data() method to set the data, which can also then be used for later retrieval (for example on a click event).
        /// </summary>
        public virtual object DT_RowData
        {
            get { return null; }
        }
    }
}