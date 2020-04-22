namespace Level_Editor
{
    partial class levelEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.gridX = new System.Windows.Forms.ComboBox();
            this.gridY = new System.Windows.Forms.ComboBox();
            this.levelNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.changeGridSize = new System.Windows.Forms.Button();
            this.shooter = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mirror = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.target = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.mirrorsAllowed = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.shooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mirror)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.target)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Edit level:";
            // 
            // gridX
            // 
            this.gridX.FormattingEnabled = true;
            this.gridX.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.gridX.Location = new System.Drawing.Point(44, 114);
            this.gridX.Name = "gridX";
            this.gridX.Size = new System.Drawing.Size(121, 24);
            this.gridX.TabIndex = 1;
            // 
            // gridY
            // 
            this.gridY.FormattingEnabled = true;
            this.gridY.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.gridY.Location = new System.Drawing.Point(44, 144);
            this.gridY.Name = "gridY";
            this.gridY.Size = new System.Drawing.Size(121, 24);
            this.gridY.TabIndex = 2;
            // 
            // levelNumber
            // 
            this.levelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelNumber.Location = new System.Drawing.Point(99, 39);
            this.levelNumber.Name = "levelNumber";
            this.levelNumber.Size = new System.Drawing.Size(66, 30);
            this.levelNumber.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Level #:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Grid size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Y:";
            // 
            // changeGridSize
            // 
            this.changeGridSize.BackColor = System.Drawing.Color.Lime;
            this.changeGridSize.Location = new System.Drawing.Point(17, 174);
            this.changeGridSize.Name = "changeGridSize";
            this.changeGridSize.Size = new System.Drawing.Size(148, 30);
            this.changeGridSize.TabIndex = 8;
            this.changeGridSize.Text = "Change";
            this.changeGridSize.UseVisualStyleBackColor = false;
            this.changeGridSize.Click += new System.EventHandler(this.ChangeGridSize_Click);
            // 
            // shooter
            // 
            this.shooter.Image = global::Level_Editor.Properties.Resources.blaster;
            this.shooter.Location = new System.Drawing.Point(853, 51);
            this.shooter.Name = "shooter";
            this.shooter.Size = new System.Drawing.Size(100, 100);
            this.shooter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.shooter.TabIndex = 10;
            this.shooter.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(852, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "Laser Shooter";
            // 
            // mirror
            // 
            this.mirror.Image = global::Level_Editor.Properties.Resources.mirror;
            this.mirror.Location = new System.Drawing.Point(1011, 51);
            this.mirror.Name = "mirror";
            this.mirror.Size = new System.Drawing.Size(100, 100);
            this.mirror.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mirror.TabIndex = 12;
            this.mirror.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1037, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "Mirror";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(878, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Target";
            // 
            // target
            // 
            this.target.Image = global::Level_Editor.Properties.Resources.target_empty;
            this.target.Location = new System.Drawing.Point(853, 188);
            this.target.Name = "target";
            this.target.Size = new System.Drawing.Size(100, 100);
            this.target.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.target.TabIndex = 14;
            this.target.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(885, 310);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 18);
            this.label9.TabIndex = 17;
            this.label9.Text = "Wall";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Level_Editor.Properties.Resources.wall_blue;
            this.pictureBox1.Location = new System.Drawing.Point(853, 331);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(999, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 17);
            this.label10.TabIndex = 18;
            this.label10.Text = "# Allowed:";
            // 
            // mirrorsAllowed
            // 
            this.mirrorsAllowed.Location = new System.Drawing.Point(1077, 157);
            this.mirrorsAllowed.Name = "mirrorsAllowed";
            this.mirrorsAllowed.Size = new System.Drawing.Size(42, 22);
            this.mirrorsAllowed.TabIndex = 19;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.Cyan;
            this.save.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save.Location = new System.Drawing.Point(17, 491);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(148, 50);
            this.save.TabIndex = 20;
            this.save.Text = "SAVE";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.Button1_Click);
            // 
            // levelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(1182, 553);
            this.Controls.Add(this.save);
            this.Controls.Add(this.mirrorsAllowed);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.target);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.mirror);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.shooter);
            this.Controls.Add(this.changeGridSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.levelNumber);
            this.Controls.Add(this.gridY);
            this.Controls.Add(this.gridX);
            this.Controls.Add(this.label1);
            this.Name = "levelEditor";
            this.Text = "Level Editor";
            ((System.ComponentModel.ISupportInitialize)(this.shooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mirror)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.target)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox gridX;
        private System.Windows.Forms.ComboBox gridY;
        private System.Windows.Forms.TextBox levelNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button changeGridSize;
        private System.Windows.Forms.PictureBox shooter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox mirror;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox target;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox mirrorsAllowed;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button save;
    }
}

