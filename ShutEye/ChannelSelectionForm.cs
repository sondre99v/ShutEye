using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDF;


namespace ShutEye
{
	public partial class ChannelSelectionForm: Form
	{
		public ChannelConfiguration[] ChannelConfigurations { get; private set; }

		private EDFSignal[] _signals;

		public ChannelSelectionForm(EDFHeader edfHeader)
		{
			InitializeComponent();

			_signals = edfHeader.Signals.ToArray();
			
		}

		private void ChannelSelectionForm_Load(object sender, EventArgs e)
		{
			foreach(EDFSignal signal in _signals)
			{
				var row = new DataGridViewRow();

				var indexCell = new DataGridViewTextBoxCell();
				indexCell.Value = signal.IndexNumber;

				var labelCell = new DataGridViewTextBoxCell();
				labelCell.Value = signal.Label;

				var showCell = new DataGridViewCheckBoxCell();
				showCell.Value = true;

				var referenceCell = new DataGridViewComboBoxCell();
				referenceCell.Items.Add("Monopolar");
				referenceCell.Items.AddRange(_signals.Select(s => s.Label).ToArray());
				referenceCell.Value = "Monopolar";

				row.Cells.Add(indexCell);
				row.Cells.Add(labelCell);
				row.Cells.Add(showCell);
				row.Cells.Add(referenceCell);

				dgvChannels.Rows.Add(row);
			}
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			ChannelConfigurations = new ChannelConfiguration[_signals.Length];
			
			for(int i = 0; i < dgvChannels.RowCount; i++)
			{
				int index = (int)dgvChannels.Rows[i].Cells[0].Value - 1;
				string label = (string)dgvChannels.Rows[i].Cells[1].Value;
				bool shown = (bool)dgvChannels.Rows[i].Cells[2].Value;
				EDFSignal reference = _signals.SingleOrDefault(s => s.Label == (string)dgvChannels.Rows[i].Cells[3].Value);

				ChannelConfigurations[i] = new ChannelConfiguration();

				ChannelConfigurations[i].Signal = _signals[index];
				ChannelConfigurations[i].Signal.Label = label;
				ChannelConfigurations[i].IsShown = shown;
				ChannelConfigurations[i].Reference = reference;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
