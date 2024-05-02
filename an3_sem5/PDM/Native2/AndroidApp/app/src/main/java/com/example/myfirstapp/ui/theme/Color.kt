package com.example.myfirstapp.ui.theme

import androidx.compose.runtime.Immutable
import androidx.compose.runtime.staticCompositionLocalOf
import androidx.compose.ui.graphics.Color

val DarkPrimary = Color(0xFFFFBCBC)
val DarkSecondary = Color(0xFFDCC2C2)
val DarkAccent = Color(0xFFEFE6B8)
val DarkPendingOp = Color.Yellow

val LightPrimary = Color(0xFFA45050)
val LightSecondary = Color(0xFF715B5B)
val LightAccent = Color(0xFF7D7652)
val LightPendingOp = Color.Red

@Immutable
data class CustomColorsPalette(
    val pendingOp: Color = Color.Unspecified
)

val LightCustomColorsPalette = CustomColorsPalette(
    pendingOp = LightPendingOp
)

val DarkCustomColorsPalette = CustomColorsPalette(
    pendingOp = DarkPendingOp
)


val LocalCustomColorsPalette = staticCompositionLocalOf { CustomColorsPalette() }
