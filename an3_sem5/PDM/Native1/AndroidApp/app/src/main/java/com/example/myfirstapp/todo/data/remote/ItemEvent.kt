package com.example.myfirstapp.todo.data.remote

import com.example.myfirstapp.todo.data.Game

data class Payload(val updatedGame: Game)
data class ItemEvent(val event: String, val payload: Payload)
