@echo off
REM Batch convert all PNGs in the current folder to WebP

SET quality=80
IF NOT EXIST webp_output (
    mkdir webp_output
)

FOR %%f IN (*.png) DO (
    echo Converting %%f ...
    cwebp -q %quality% "%%f" -o "webp_output/%%~nf.webp"
)

echo.
echo All done! Converted files are in the "webp_output" folder.
pause
