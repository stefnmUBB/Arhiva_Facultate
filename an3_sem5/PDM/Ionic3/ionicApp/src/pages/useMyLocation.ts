import { useEffect, useState } from 'react';
import { Position, Geolocation } from '@capacitor/geolocation';

interface MyLocation {
    position?: Position;
    error?: Error;
}

export const useMyLocation = () => {
    const [state, setState] = useState<MyLocation>({});
    useEffect(watchMyLocation, []);
    return state;

    function watchMyLocation() {
        let cancelled = false;
        Geolocation.getCurrentPosition()
            .then(position => updateMyPosition('current', position))
            .catch(error => updateMyPosition('current',undefined, error));
        const callbackId = Geolocation.watchPosition({}, (position, error) => {
            updateMyPosition('watch', position!, error);
        });
        return ()=>{ async () => {
            cancelled = true;
            Geolocation.clearWatch({ id: await callbackId });
        }
        };

        function updateMyPosition(source: string, position?: Position, error: any = undefined) {
            console.log(source, position, error);
            if (!cancelled) {
                setState({ ...state, position: position || state.position, error });
            }
        }
    }
};
