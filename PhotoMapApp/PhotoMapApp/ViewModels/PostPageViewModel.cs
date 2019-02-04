using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using PhotoMapApp.Models;
using Xamarin.Forms;
using PhotoMapApp.Services.Definitions;

namespace PhotoMapApp.ViewModels
{
    public class PostPageViewModel : ViewModelBase
    {
        private Post _post;
        public Post Post { get { return this._post; } set { SetProperty(ref this._post, value); }}
        public DelegateCommand DeletePostDelegate { get; private set; }
        public DelegateCommand EditPostDelegate { get; private set; }
        private IImageService _imageService;

        public ImageSource BannerImageSource { get; private set; }
        public ImageSource EditButtonImageSource { get; private set; }
        public ImageSource DeleteButtonImageSource { get; private set; }

        private IPageDialogService _dialogService;
        public PostPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IImageService imageService) : base(navigationService)
        {
            this._imageService = imageService;
            this._dialogService = dialogService;
            this.DeletePostDelegate = new DelegateCommand(DeletePostCommand);
            this.EditPostDelegate = new DelegateCommand(EditPostCommand);

            // Resources
            this.BannerImageSource = this._imageService.GetSource("profil.png");
            this.EditButtonImageSource = this._imageService.GetSource("Icons.edit.png");
            this.DeleteButtonImageSource = this._imageService.GetSource("Icons.delete.png");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.Post = (Post)parameters["post"];
            this.Title = Post.Name;
        }

        private async void DeletePostCommand()
        {
            var answer = await _dialogService.DisplayAlertAsync("Suppression de post", "Voulez-vous vraiment supprimer ce post ?", "Supprimer", "Annuler");
            if (answer == true)
            {
                // TODO : SUPPRIMER LE POST
                await _dialogService.DisplayAlertAsync("Alert", "OUI", "ok");
            }
        }

        private void EditPostCommand()
        {
           // TODO : Ouvrir page de modif
        }
    }
}
