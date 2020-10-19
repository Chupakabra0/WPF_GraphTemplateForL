using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;
using RKMApp.Basic;

namespace WpfApp1 {
    public class WindowVM : BaseVM {
        public WindowVM() {
            this.model = new PlotModel { Title = "ФАК ЭТА ХУЙНЯ РАБОТАЕТ!", Series = { new AreaSeries() }};
            this.Collection = new ObservableCollection<Tuple<double, double>> {
                new Tuple<double, double>(0.0, 0.0),
                new Tuple<double, double>(0.5, 2.0),
                new Tuple<double, double>(1.0, 3.0),
                new Tuple<double, double>(2.0, -4.0),
                new Tuple<double, double>(1.0, 1.0)
            };

            this.PressMe = new AsyncCommand(async () => {
                this.IsPressMe = true;
                await Task.Run(() => {
                    this.model.Series.Add(this.CreateLine(this.Collection, OxyColor.FromRgb(255, 0, 0), "Прямая"));
                    this.model.InvalidatePlot(true);
                });
                this.IsPressMe = false;
            }, () => !this.IsPressMe );
        }

        public PlotModel model { get; set; }

        // Тут твои точки, которые ты получила после метода Рунге-Кутты 
        public ObservableCollection<Tuple<double, double>> Collection { get; set; }
        public double                                      X0         { get; set; }
        public double                                      Y0         { get; set; }
        public double                                      H          { get; set; }
        public double                                      A          { get; set; }
        public double                                      B          { get; set; }
        // Твоя функция (правая часть диффуры)
        public Func<double, double, double>                Function = (x, y) => 0.8 * x * y - x;

        public ICommand PressMe   { get; }
        public bool     IsPressMe { get; private set; }

        // Рисует кривую
        private LineSeries CreateLine(IEnumerable<Tuple<double, double>> dotsList, OxyColor color, string title) {
            var line = new LineSeries { Color = color, Title = title };

            foreach (var xy in dotsList) {
                line.Points.Add(new DataPoint(xy.Item1, xy.Item2));
            }

            return line;
        }
    }
}
