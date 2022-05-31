using System.Collections.Generic;

namespace Utility.GlobalModels
{
    /*
        jika filter = equal, maka searchText yang digunakan adalah searchText2
    */

    public class ParameterSearchModel
    {
        public string columnName { get; set; }
        public string filter { get; set; }
        public string searchText { get; set; }
        public string searchText2 { get; set; }
    }

    public class ParameterSearchWithLimit
    {
        public List<ParameterSearchModel> paramSearch { get; set; }
        public int row_count { get; set; } //jumlah data per halaman
        private int _page { get; set; }
        public int page  //halaman ke
        {
            get
            {
                return this._page;
            }
            
            set
            {
                this._page = row_count * (value - 1);
            }
        }
    }
}