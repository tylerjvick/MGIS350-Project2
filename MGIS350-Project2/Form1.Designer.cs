namespace MGIS350_Project2
{
    partial class Form1
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
            this.btnAddInv = new System.Windows.Forms.Button();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.grpSize = new System.Windows.Forms.GroupBox();
            this.grpTopping = new System.Windows.Forms.GroupBox();
            this.lstAddInv = new System.Windows.Forms.ListBox();
            this.lstPreview = new System.Windows.Forms.ListBox();
            this.lblAddInv = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.nudAddInv = new System.Windows.Forms.NumericUpDown();
            this.rdoLarge = new System.Windows.Forms.RadioButton();
            this.rdoMed = new System.Windows.Forms.RadioButton();
            this.rdoSmall = new System.Windows.Forms.RadioButton();
            this.chkExCheese = new System.Windows.Forms.CheckBox();
            this.chkPepperoni = new System.Windows.Forms.CheckBox();
            this.chkMushroom = new System.Windows.Forms.CheckBox();
            this.chkSausage = new System.Windows.Forms.CheckBox();
            this.grpSize.SuspendLayout();
            this.grpTopping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAddInv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddInv
            // 
            this.btnAddInv.Location = new System.Drawing.Point(12, 53);
            this.btnAddInv.Name = "btnAddInv";
            this.btnAddInv.Size = new System.Drawing.Size(108, 23);
            this.btnAddInv.TabIndex = 0;
            this.btnAddInv.Text = "Add to Inventory";
            this.btnAddInv.UseVisualStyleBackColor = true;
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.Location = new System.Drawing.Point(273, 246);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(75, 23);
            this.btnAddOrder.TabIndex = 1;
            this.btnAddOrder.Text = "Add to Order";
            this.btnAddOrder.UseVisualStyleBackColor = true;
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.Location = new System.Drawing.Point(11, 396);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(75, 23);
            this.btnPlaceOrder.TabIndex = 2;
            this.btnPlaceOrder.Text = "Place Order";
            this.btnPlaceOrder.UseVisualStyleBackColor = true;
            // 
            // grpSize
            // 
            this.grpSize.Controls.Add(this.rdoSmall);
            this.grpSize.Controls.Add(this.rdoMed);
            this.grpSize.Controls.Add(this.rdoLarge);
            this.grpSize.Location = new System.Drawing.Point(12, 140);
            this.grpSize.Name = "grpSize";
            this.grpSize.Size = new System.Drawing.Size(108, 100);
            this.grpSize.TabIndex = 3;
            this.grpSize.TabStop = false;
            this.grpSize.Text = "Pizza Size";
            // 
            // grpTopping
            // 
            this.grpTopping.Controls.Add(this.chkSausage);
            this.grpTopping.Controls.Add(this.chkMushroom);
            this.grpTopping.Controls.Add(this.chkPepperoni);
            this.grpTopping.Controls.Add(this.chkExCheese);
            this.grpTopping.Location = new System.Drawing.Point(126, 140);
            this.grpTopping.Name = "grpTopping";
            this.grpTopping.Size = new System.Drawing.Size(141, 129);
            this.grpTopping.TabIndex = 4;
            this.grpTopping.TabStop = false;
            this.grpTopping.Text = "Toppings";
            // 
            // lstAddInv
            // 
            this.lstAddInv.FormattingEnabled = true;
            this.lstAddInv.Location = new System.Drawing.Point(180, 27);
            this.lstAddInv.Name = "lstAddInv";
            this.lstAddInv.Size = new System.Drawing.Size(168, 95);
            this.lstAddInv.TabIndex = 5;
            // 
            // lstPreview
            // 
            this.lstPreview.FormattingEnabled = true;
            this.lstPreview.Location = new System.Drawing.Point(12, 295);
            this.lstPreview.Name = "lstPreview";
            this.lstPreview.Size = new System.Drawing.Size(336, 95);
            this.lstPreview.TabIndex = 6;
            // 
            // lblAddInv
            // 
            this.lblAddInv.AutoSize = true;
            this.lblAddInv.Location = new System.Drawing.Point(12, 30);
            this.lblAddInv.Name = "lblAddInv";
            this.lblAddInv.Size = new System.Drawing.Size(85, 13);
            this.lblAddInv.TabIndex = 7;
            this.lblAddInv.Text = "Add to Inventory";
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(12, 279);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(74, 13);
            this.lblPreview.TabIndex = 8;
            this.lblPreview.Text = "Order Preview";
            // 
            // nudAddInv
            // 
            this.nudAddInv.Location = new System.Drawing.Point(103, 27);
            this.nudAddInv.Name = "nudAddInv";
            this.nudAddInv.Size = new System.Drawing.Size(71, 20);
            this.nudAddInv.TabIndex = 9;
            // 
            // rdoLarge
            // 
            this.rdoLarge.AutoSize = true;
            this.rdoLarge.Location = new System.Drawing.Point(3, 20);
            this.rdoLarge.Name = "rdoLarge";
            this.rdoLarge.Size = new System.Drawing.Size(52, 17);
            this.rdoLarge.TabIndex = 0;
            this.rdoLarge.TabStop = true;
            this.rdoLarge.Text = "Large";
            this.rdoLarge.UseVisualStyleBackColor = true;
            // 
            // rdoMed
            // 
            this.rdoMed.AutoSize = true;
            this.rdoMed.Location = new System.Drawing.Point(3, 45);
            this.rdoMed.Name = "rdoMed";
            this.rdoMed.Size = new System.Drawing.Size(62, 17);
            this.rdoMed.TabIndex = 1;
            this.rdoMed.TabStop = true;
            this.rdoMed.Text = "Medium";
            this.rdoMed.UseVisualStyleBackColor = true;
            // 
            // rdoSmall
            // 
            this.rdoSmall.AutoSize = true;
            this.rdoSmall.Location = new System.Drawing.Point(3, 68);
            this.rdoSmall.Name = "rdoSmall";
            this.rdoSmall.Size = new System.Drawing.Size(50, 17);
            this.rdoSmall.TabIndex = 2;
            this.rdoSmall.TabStop = true;
            this.rdoSmall.Text = "Small";
            this.rdoSmall.UseVisualStyleBackColor = true;
            // 
            // chkExCheese
            // 
            this.chkExCheese.AutoSize = true;
            this.chkExCheese.Location = new System.Drawing.Point(7, 20);
            this.chkExCheese.Name = "chkExCheese";
            this.chkExCheese.Size = new System.Drawing.Size(89, 17);
            this.chkExCheese.TabIndex = 0;
            this.chkExCheese.Text = "Extra Cheese";
            this.chkExCheese.UseVisualStyleBackColor = true;
            // 
            // chkPepperoni
            // 
            this.chkPepperoni.AutoSize = true;
            this.chkPepperoni.Location = new System.Drawing.Point(7, 45);
            this.chkPepperoni.Name = "chkPepperoni";
            this.chkPepperoni.Size = new System.Drawing.Size(74, 17);
            this.chkPepperoni.TabIndex = 1;
            this.chkPepperoni.Text = "Pepperoni";
            this.chkPepperoni.UseVisualStyleBackColor = true;
            // 
            // chkMushroom
            // 
            this.chkMushroom.AutoSize = true;
            this.chkMushroom.Location = new System.Drawing.Point(7, 68);
            this.chkMushroom.Name = "chkMushroom";
            this.chkMushroom.Size = new System.Drawing.Size(80, 17);
            this.chkMushroom.TabIndex = 2;
            this.chkMushroom.Text = "Mushrooms";
            this.chkMushroom.UseVisualStyleBackColor = true;
            // 
            // chkSausage
            // 
            this.chkSausage.AutoSize = true;
            this.chkSausage.Location = new System.Drawing.Point(7, 92);
            this.chkSausage.Name = "chkSausage";
            this.chkSausage.Size = new System.Drawing.Size(68, 17);
            this.chkSausage.TabIndex = 3;
            this.chkSausage.Text = "Sausage";
            this.chkSausage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 428);
            this.Controls.Add(this.nudAddInv);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.lblAddInv);
            this.Controls.Add(this.lstPreview);
            this.Controls.Add(this.lstAddInv);
            this.Controls.Add(this.grpTopping);
            this.Controls.Add(this.grpSize);
            this.Controls.Add(this.btnPlaceOrder);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.btnAddInv);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpSize.ResumeLayout(false);
            this.grpSize.PerformLayout();
            this.grpTopping.ResumeLayout(false);
            this.grpTopping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAddInv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddInv;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.GroupBox grpSize;
        private System.Windows.Forms.RadioButton rdoSmall;
        private System.Windows.Forms.RadioButton rdoMed;
        private System.Windows.Forms.RadioButton rdoLarge;
        private System.Windows.Forms.GroupBox grpTopping;
        private System.Windows.Forms.CheckBox chkSausage;
        private System.Windows.Forms.CheckBox chkMushroom;
        private System.Windows.Forms.CheckBox chkPepperoni;
        private System.Windows.Forms.CheckBox chkExCheese;
        private System.Windows.Forms.ListBox lstAddInv;
        private System.Windows.Forms.ListBox lstPreview;
        private System.Windows.Forms.Label lblAddInv;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.NumericUpDown nudAddInv;
    }
}

