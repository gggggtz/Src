﻿Param
(
    
    [String] $src,
    [String] $dest
)

Move-Item -Path $src -Destination $dest -Force