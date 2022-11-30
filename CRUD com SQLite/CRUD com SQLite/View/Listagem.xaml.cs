using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CRUD_com_SQLite.Model;

namespace CRUD_com_SQLite.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listagem : ContentPage
    {
        ObservableCollection<produto> lista_produtos = new ObservableCollection<produto>();
        public Listagem()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produtos;
        }

        private void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoProduto());
            } 
            catch (Exception ex)
            {
                DisplayAlert("Opa", ex.Message, "Ok");
            }
        }

        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.preco * i.quantidade);
            string msg = "O total da compra é: " + soma;

            DisplayAlert("Opa", msg, "Ok");
        }
        protected override void OnAppearing()
        {
            if(lista_produtos.Count == 0)
            {
                System.Threading.Tasks.Task.Run(async () =>
                {
                    List<produto> temp = await App.Database.getAll();
                    foreach (produto item in temp)
                    {
                        lista_produtos.Add(item);
                    }
                    ref_carregando.IsRefreshing = false;
                });
            }            
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem)sender;
            produto produto_selecionado = (produto)disparador.BindingContext;

            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");
            if (confirmacao)
            {
                await App.Database.delete(produto_selecionado.id);
                lista_produtos.Remove(produto_selecionado);
            }
        }

        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;
            System.Threading.Tasks.Task.Run(async () =>
            {
                List<produto> temp = await App.Database.Search(buscou);

                lista_produtos.Clear();

                foreach (produto item in temp)
                {
                    lista_produtos.Add(item);
                }
                ref_carregando.IsRefreshing = false;
            });

        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new EditarProduto
            {
                BindingContext = (produto)e.SelectedItem
            });
        }
    }
}