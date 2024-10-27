namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bigTextComponent1 = new LabLibrary2.BigTextComponent(components);
            buttonCreateDoc = new Button();
            buttonCreateTable = new Button();
            tableComponent1 = new LabLibrary2.TableComponent(components);
            SuspendLayout();
            // 
            // buttonCreateDoc
            // 
            buttonCreateDoc.Location = new Point(203, 30);
            buttonCreateDoc.Name = "buttonCreateDoc";
            buttonCreateDoc.Size = new Size(147, 56);
            buttonCreateDoc.TabIndex = 0;
            buttonCreateDoc.Text = "Создать документ";
            buttonCreateDoc.UseVisualStyleBackColor = true;
            buttonCreateDoc.Click += buttonCreateDoc_Click;
            // 
            // buttonCreateTable
            // 
            buttonCreateTable.Location = new Point(375, 30);
            buttonCreateTable.Name = "buttonCreateTable";
            buttonCreateTable.Size = new Size(147, 56);
            buttonCreateTable.TabIndex = 1;
            buttonCreateTable.Text = "Создать таблицу";
            buttonCreateTable.UseVisualStyleBackColor = true;
            buttonCreateTable.Click += buttonCreateTable_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonCreateTable);
            Controls.Add(buttonCreateDoc);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private LabLibrary2.BigTextComponent bigTextComponent1;
        private Button buttonCreateDoc;
        private Button buttonCreateTable;
        private LabLibrary2.TableComponent tableComponent1;
    }
}
