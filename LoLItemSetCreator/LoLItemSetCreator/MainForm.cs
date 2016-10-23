using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LoLItemSetCreator.DTOs;
using LoLItemSetCreator.Repositories;
using LoLItemSetCreator.Services;

namespace LoLItemSetCreator
{
    public partial class MainForm : Form
    {
        private readonly ItemService _itemService;

        public MainForm()
        {
            _itemService = new ItemService(new RiotStaticRepository());

            InitializeComponent();

            var items = _itemService.GetItems();

            foreach (var item in items)
            {
                this.mainPanel.Controls.Add(CreatePictureBox(item));
            }
        }

        private Control CreatePictureBox(Item item)
        {
            var image = Image.FromStream(_itemService.GetPictureStream(item.Id));

            var pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            return pictureBox;
        }
    }
}
