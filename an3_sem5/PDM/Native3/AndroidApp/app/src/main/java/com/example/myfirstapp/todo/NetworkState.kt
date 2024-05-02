package com.example.myfirstapp.todo

import android.annotation.SuppressLint
import android.content.Context
import android.net.ConnectivityManager
import android.net.Network
import android.net.NetworkCapabilities
import android.net.NetworkRequest
import android.os.Build
import androidx.compose.runtime.Composable
import androidx.compose.runtime.State
import androidx.compose.runtime.produceState
import androidx.compose.ui.platform.LocalContext
import androidx.core.net.ConnectivityManagerCompat
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.channels.awaitClose
import kotlinx.coroutines.flow.callbackFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.flow.flowOn
import java.util.logging.Level
import java.util.logging.Logger

private data class NetworkState(
    val isConnected: Boolean,
    val isValidated: Boolean,
    val isMetered: Boolean,
    val isNotRoaming: Boolean
)

sealed class ConnectionState {
    object Available : ConnectionState()
    object Unavailable : ConnectionState()
}

@Suppress("DEPRECATION")
private val ConnectivityManager.activeNetworkState: NetworkState
    @SuppressLint("MissingPermission")
    get() {
        // Use getActiveNetworkInfo() instead of getNetworkInfo(network) because it can detect VPNs.
        val info = activeNetworkInfo
        val isConnected = info != null && info.isConnected
        val isValidated = isActiveNetworkValidated
        val isMetered = ConnectivityManagerCompat.isActiveNetworkMetered(this)
        val isNotRoaming = info != null && !info.isRoaming
        return NetworkState(isConnected, isValidated, isMetered, isNotRoaming)
    }

private val ConnectivityManager.isActiveNetworkValidated: Boolean
    @SuppressLint("RestrictedApi", "MissingPermission")
    get() =
        try {
            val network = activeNetwork
            val capabilities = getNetworkCapabilities(network)
            (capabilities?.hasCapability(NetworkCapabilities.NET_CAPABILITY_VALIDATED)) ?: false
        } catch (exception: SecurityException) {
            Logger.getLogger("NetworkStateTracker").log(Level.INFO,"Unable to validate active network")
            false
        }

/** Network Utility to observe Internet connectivity status */
@SuppressLint("MissingPermission")
@ExperimentalCoroutinesApi
private fun Context.observeConnectivityAsFlow() =
    callbackFlow {
        val connectivityManager =
            getSystemService(Context.CONNECTIVITY_SERVICE) as ConnectivityManager

        fun sendConnectionState() =
            with(connectivityManager.activeNetworkState) {
                if (isConnected && isValidated) {
                    trySend(ConnectionState.Available)
                } else {
                    trySend(ConnectionState.Unavailable)
                }
            }

        val networkRequest =
            NetworkRequest.Builder()
                .addCapability(NetworkCapabilities.NET_CAPABILITY_INTERNET)
                .addTransportType(NetworkCapabilities.TRANSPORT_WIFI)
                .addTransportType(NetworkCapabilities.TRANSPORT_CELLULAR)
                .build()

        val networkCallback =
            object : ConnectivityManager.NetworkCallback() {
                // satisfies network capability and transport requirements requested in networkRequest
                override fun onCapabilitiesChanged(
                    network: Network,
                    networkCapabilities: NetworkCapabilities
                ) {
                    sendConnectionState()
                }
                override fun onLost(network: Network) {
                    sendConnectionState()
                }
            }

        connectivityManager.registerNetworkCallback(networkRequest, networkCallback)

        awaitClose {
            connectivityManager.unregisterNetworkCallback(networkCallback)
        }
    }
        .distinctUntilChanged()
        .flowOn(Dispatchers.IO)

@ExperimentalCoroutinesApi
@Composable
fun connectivityState(): State<ConnectionState> {
    val context = LocalContext.current
    return produceState<ConnectionState>(initialValue = ConnectionState.Unavailable) {
        context.observeConnectivityAsFlow().collect { value = it }
    }
}