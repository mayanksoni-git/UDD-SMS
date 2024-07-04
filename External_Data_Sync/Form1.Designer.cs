namespace External_Data_Sync
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
            this.btnFinancialYear = new System.Windows.Forms.Button();
            this.btnScheme = new System.Windows.Forms.Button();
            this.btnDistrict = new System.Windows.Forms.Button();
            this.btnULB = new System.Windows.Forms.Button();
            this.btnWork = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFinancialYear
            // 
            this.btnFinancialYear.Location = new System.Drawing.Point(12, 12);
            this.btnFinancialYear.Name = "btnFinancialYear";
            this.btnFinancialYear.Size = new System.Drawing.Size(91, 74);
            this.btnFinancialYear.TabIndex = 0;
            this.btnFinancialYear.Text = "Financial Year";
            this.btnFinancialYear.UseVisualStyleBackColor = true;
            this.btnFinancialYear.Click += new System.EventHandler(this.btnFinancialYear_Click);
            // 
            // btnScheme
            // 
            this.btnScheme.Location = new System.Drawing.Point(109, 12);
            this.btnScheme.Name = "btnScheme";
            this.btnScheme.Size = new System.Drawing.Size(91, 74);
            this.btnScheme.TabIndex = 1;
            this.btnScheme.Text = "Scheme";
            this.btnScheme.UseVisualStyleBackColor = true;
            this.btnScheme.Click += new System.EventHandler(this.btnScheme_Click);
            // 
            // btnDistrict
            // 
            this.btnDistrict.Location = new System.Drawing.Point(206, 12);
            this.btnDistrict.Name = "btnDistrict";
            this.btnDistrict.Size = new System.Drawing.Size(91, 74);
            this.btnDistrict.TabIndex = 2;
            this.btnDistrict.Text = "District";
            this.btnDistrict.UseVisualStyleBackColor = true;
            this.btnDistrict.Click += new System.EventHandler(this.btnDistrict_Click);
            // 
            // btnULB
            // 
            this.btnULB.Location = new System.Drawing.Point(303, 12);
            this.btnULB.Name = "btnULB";
            this.btnULB.Size = new System.Drawing.Size(91, 74);
            this.btnULB.TabIndex = 3;
            this.btnULB.Text = "ULB";
            this.btnULB.UseVisualStyleBackColor = true;
            this.btnULB.Click += new System.EventHandler(this.btnULB_Click);
            // 
            // btnWork
            // 
            this.btnWork.Location = new System.Drawing.Point(400, 12);
            this.btnWork.Name = "btnWork";
            this.btnWork.Size = new System.Drawing.Size(91, 74);
            this.btnWork.TabIndex = 4;
            this.btnWork.Text = "Work";
            this.btnWork.UseVisualStyleBackColor = true;
            this.btnWork.Click += new System.EventHandler(this.btnWork_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnWork);
            this.Controls.Add(this.btnULB);
            this.Controls.Add(this.btnDistrict);
            this.Controls.Add(this.btnScheme);
            this.Controls.Add(this.btnFinancialYear);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFinancialYear;
        private System.Windows.Forms.Button btnScheme;
        private System.Windows.Forms.Button btnDistrict;
        private System.Windows.Forms.Button btnULB;
        private System.Windows.Forms.Button btnWork;
    }
}

