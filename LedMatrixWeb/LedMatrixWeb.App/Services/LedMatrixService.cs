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

        private int brightness = 25;

        public void SetPixel(int x, int y, Color color)
        {
            pixels[x, y] = color;
        }

        public void SetBrightness(int newBrightness)
        {
            brightness = newBrightness;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (var row = 0; row < 32; row++)
            {
                for (var col = 0; col < 32; col++)
                {
                    pixels[col, row] = new Color(1, 1, 1);
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
                matrix.Brightness = Convert.ToByte(brightness);
                
                for(var row = 0; row < 32; row++)
                {
                    for(var col = 0; col < 32; col++)
                    {
                        canvas.SetPixel(col, row, pixels[col, row]);
                    }
                }

                canvas = matrix.SwapOnVsync(canvas);

                await Task.Delay(1);
            } while (stoppingToken.IsCancellationRequested == false);
        }
    }
}
