package com.example.myfirstapp.todo.data

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.Query
import androidx.room.Update

@Dao
interface GameDao {
    @Query("SELECT * FROM Game")
    fun getAll(): List<Game>

    @Query("SELECT * FROM Game WHERE _id == :id")
    fun getById(id: String): List<Game>

    @Insert
    fun insert(game: Game)

    @Update
    fun update(game: Game)

    @Query("DELETE FROM Game")
    fun clear()

    @Query("DELETE FROM Game WHERE _id == :id")
    fun deleteById(id:String)
}