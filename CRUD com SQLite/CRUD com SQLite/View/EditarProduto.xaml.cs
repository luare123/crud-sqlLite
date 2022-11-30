using CRUD_com_SQLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRUD_com_SQLite.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                produto produto_anexado = BindingContext as produto;
                produto p = new produto
                {
                    id = produto_anexado.id,
                    descricao = txt_descricao.Text,
                    quantidade = Convert.ToDouble(txt_qntd.Text),
                    preco = Convert.ToDouble(txt_preco.Text)
                };
                await App.Database.update(p);
                await DisplayAlert("Sucesso!", "Produto Editado", "Ok");
                await Navigation.PushAsync(new Listagem());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Opa", ex.Message, "Ok");
            }

        }
    }
}