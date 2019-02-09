using Microsoft.Extensions.Hosting;
using rpi_rgb_led_matrix_sharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LedMatrixWeb.App.Services
{
    class LedMatrixService : BackgroundService
    {
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

            var keepRunning = true;

            do
            {
                canvas.Clear();

                canvas.SetPixel(5, 5, new Color(254, 254, 254));

                canvas = matrix.SwapOnVsync(canvas);

                await Task.Delay(1);
            } while (stoppingToken.IsCancellationRequested);

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
