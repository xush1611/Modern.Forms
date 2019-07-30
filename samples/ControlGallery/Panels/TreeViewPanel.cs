﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Modern.Forms;

namespace ControlGallery.Panels
{
    public class TreeViewPanel : Panel
    {
        private int max_dirs = 5;

        public TreeViewPanel ()
        {
            var tree = new TreeView {
                Left = 10,
                Top = 10
            };

            foreach (var drive in DriveInfo.GetDrives ().Where (d => d.IsReady)) {
                var tvi = CreateDirectoryNode (drive.Name);

                tvi.Text = $"{drive.Name.Trim ('\\')} - {drive.VolumeLabel}";
                tvi.Image = ImageLoader.Get ("drive.png");
                tvi.Tag = drive;

                tree.Items.Add (tvi);
            }

            tree.Items[0].Expanded = true;
            tree.Items[1].Expanded = true;
            tree.Items[2].Expanded = true;
            Controls.Add (tree);

            var show_dropdowns = Controls.Add (new CheckBox {
                Text = "Show Dropdown Glyph",
                Checked = true,
                Left = 550,
                Top = 10,
                Width = 200
            });

            show_dropdowns.CheckedChanged += (o, e) => tree.ShowDropdownGlyph = show_dropdowns.Checked;

            var show_images = Controls.Add (new CheckBox {
                Text = "Show Images",
                Checked = true,
                Left = 550,
                Top = 40,
                Width = 200
            });

            show_images.CheckedChanged += (o, e) => tree.ShowItemImages = show_images.Checked;
        }

        private TreeViewItem CreateDirectoryNode (string path)
        {
            var tvi = new TreeViewItem (Path.GetFileName (path)) { Image = ImageLoader.Get ("folder.png") };

            try {
                foreach (var dir in Directory.EnumerateDirectories (path).Take (max_dirs))
                    tvi.Items.Add (CreateDirectoryNode (dir));
            } catch (Exception) {

            }

            return tvi;
        }
    }
}
