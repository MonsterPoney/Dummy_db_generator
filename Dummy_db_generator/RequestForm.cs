using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dummy_db_generator {
    public partial class RequestForm : Form {
        public RequestForm(string req="") {
            InitializeComponent();
            Box_request.Text = req;
        }
    }
}
