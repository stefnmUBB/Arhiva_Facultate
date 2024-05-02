import { useEffect } from 'react';
import { Preferences } from '@capacitor/preferences';

export const usePreferences = () => {
  useEffect(() => {
    runPreferencesDemo();
  }, []);

  function runPreferencesDemo() {
    (async () => {
      // Saving ({ key: string, value: string }) => Promise<void>
      await Preferences.set({
        key: 'user',
        value: JSON.stringify({
          username: 'a', password: 'a',
        })
      });

      // Loading value by key ({ key: string }) => Promise<{ value: string | null }>
      const res = await Preferences.get({ key: 'user' });
      if (res.value) {
        console.log('User found', JSON.parse(res.value));
      } else {
        console.log('User not found');
      }

      // Loading keys () => Promise<{ keys: string[] }>
      const { keys } = await Preferences.keys();
      console.log('Keys found', keys);

      // Removing value by key, ({ key: string }) => Promise<void>
      await Preferences.remove({ key: 'user' });
      console.log('Keys found after remove', await Preferences.keys());

      // Clear storage () => Promise<void>
      await Preferences.clear();
    })();
  }
};
