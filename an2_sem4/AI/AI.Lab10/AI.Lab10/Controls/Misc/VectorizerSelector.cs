using AI.Lab10.Tools.Text.Vectorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab10.Controls.Misc
{
    public partial class VectorizerSelector : UserControl
    {
        public VectorizerSelector()
        {
            InitializeComponent();
        }

        public string SelectedName => Combobox.SelectedItem as string;

        public IVectorizer Vectorizer
        {
            get
            {
                var item = Combobox.SelectedItem as string;
                if (item == "BagOfWords")
                    return new BagOfWords();
                if (item == "NGram2")
                    return new NGram(2);
                if (item == "NGram3")
                    return new NGram(3);
                if (item == "NGram4")
                    return new NGram(4);
                if (item == "TFIDF")
                    return new TFIDF();
                if (item == "Word2Vec")
                    return new Word2Vec();
                return new BagOfWords();
            }
        }

        private void Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VectorizerChanged?.Invoke();
        }

        public delegate void OnVectorizerChanged();
        public event OnVectorizerChanged VectorizerChanged;
    }
}
