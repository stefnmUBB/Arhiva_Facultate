package com.example.myfirstapp.todo.data.remote

import com.example.myfirstapp.todo.data.Game
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.Headers
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path

interface ItemService {
    @GET("/api/game")
    suspend fun find(@Header("Authorization") authorization: String): List<Game>

    @GET("/api/game/{id}")
    suspend fun read( @Header("Authorization") authorization: String, @Path("id") itemId: String?): Game;

    @Headers("Content-Type: application/json")
    @POST("/api/game")
    suspend fun create(@Header("Authorization") authorization: String, @Body game: Game): Game

    @Headers("Content-Type: application/json")
    @PUT("/api/game/{id}")
    suspend fun update( @Header("Authorization") authorization: String, @Path("id") itemId: String?, @Body game: Game): Game
}
