using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CRUD_com_SQLite.Model;

namespace CRUD_com_SQLite.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                produto p = new produto
                {
                    descricao = txt_descricao.Text,
                    quantidade = Convert.ToDouble(txt_qntd.Text),
                    preco = Convert.ToDouble(txt_preco.Text)
                };
                await App.Database.insert(p);
                await DisplayAlert("Sucesso!", "Produto Cadastrado", "Ok");
                await Navigation.PushAsync(new Listagem());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Opa", ex.Message, "Ok");
            }
        }
    }
}