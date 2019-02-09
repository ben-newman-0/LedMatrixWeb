using Microsoft.Extensions.Hosting;
using rpi_rgb_led_matrix_sharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LedMatrixWeb.App.Services
{
    class LedMatrixService : BackgroundService
    {
        public int X { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var options = new RGBLedMatrixOptions
            {
                Cols = 32,
                Rows = 16,
                ChainLength = 2,
                PixelMapperConfig = "BottomToTop-Mapper",
                DisableHardwarePulsing = true,
                HardwareMapping = "adafruit-hat",
                Brightness = 25
            };

            var matrix = new RGBLedMatrix(options);
            var canvas = matrix.CreateOffscreenCanvas();

            do
            {
                canvas.Clear();

                canvas.SetPixel(X, 5, new Color(254, 254, 254));

                canvas = matrix.SwapOnVsync(canvas);

                Console.Write(X + " ");
                await Task.Delay(1);
            } while (stoppingToken.IsCancellationRequested == false);

            //x = 2;

            //var options = new RGBLedMatrixOptions
            //{
            //    Cols = 32,
            //    Rows = 16,
            //    ChainLength = 2,
            //    PixelMapperConfig = "BottomToTop-Mapper",
            //    DisableHardwarePulsing = true,
            //    HardwareMapping = "adafruit-hat",
            //    Brightness = 25
            //};

            //var matrix = new RGBLedMatrix(options);
            //var canvas = matrix.CreateOffscreenCanvas();

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    canvas.Clear();

            //    canvas.SetPixel(2, 2, new Color(255, 255, 255));

            //    canvas = matrix.SwapOnVsync(canvas);

            //    await Task.Delay(1);
            //}
        }
    }
}
