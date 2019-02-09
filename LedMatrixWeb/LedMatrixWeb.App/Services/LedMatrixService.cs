using Microsoft.Extensions.Hosting;
using rpi_rgb_led_matrix_sharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LedMatrixWeb.App.Services
{
    class LedMatrixService : BackgroundService
    {
        private Color[,] pixels = new Color[32, 32];

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (var row = 0; row < 32; row++)
            {
                for (var col = 0; col < 32; col++)
                {
                    pixels[col, row] = new Color(255, 255, 255);
                }
            }

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

                for(var row = 0; row < 32; row++)
                {
                    for(var col = 0; col < 32; col++)
                    {
                        canvas.SetPixel(col, row, pixels[col, row]);
                    }
                }

                canvas = matrix.SwapOnVsync(canvas);

                Console.Write(".");
                await Task.Delay(1);
            } while (stoppingToken.IsCancellationRequested == false);
        }
    }
}
