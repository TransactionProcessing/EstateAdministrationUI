namespace EstateAdministrationUI.Areas.Estate.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTablesResult<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        [JsonProperty("draw")]
        public Int32 Draw { get; set; }

        /// <summary>
        /// Gets or sets the records filtered.
        /// </summary>
        /// <value>
        /// The records filtered.
        /// </value>
        [JsonProperty("recordsFiltered")]
        public Int32 RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the records total.
        /// </summary>
        /// <value>
        /// The records total.
        /// </value>
        [JsonProperty("recordsTotal")]
        public Int32 RecordsTotal { get; set; }

        #endregion
    }
}