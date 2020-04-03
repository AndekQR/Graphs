using Client.Service;
using Client.View;

namespace Client.Presenter {

    public class MainPresenter {
        // korzysta z repozytrium zeby uzyskac dane i je zapisywać
        // zarządza widokiem

        private IMainView mainView;
        private MainRepository repository;

        public MainPresenter(IMainView mainView) {
            this.mainView = mainView;
            repository = new MainRepository();
        }

        public void makeFullName() {
            string firstName = mainView.PersonName;
            string lastName = mainView.LastName;
            string fullName = firstName + " " + lastName;
            mainView.FullName = fullName;
        }
    }
}