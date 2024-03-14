package com.bluezzzy.recognissimo.unity.utils;

import android.util.Log;
import android.content.Context;
import android.os.storage.OnObbStateChangeListener;
import android.os.storage.StorageManager;

public class ObbMounter {
    private static final String TAG = "ObbMounter";

    private boolean isMounted;

    private static StorageManager sm;
    
    private boolean loading;
    
    private String mountedObbPath;

    public ObbMounter(final Context context, final String obbPath) {
        sm = (StorageManager)context.getSystemService(Context.STORAGE_SERVICE);
        
        if (sm.isObbMounted(obbPath)) {
            isMounted = true;
            loading = false;
            
            mountedObbPath = sm.getMountedObbPath(obbPath);
            
            return;
        }
        
        isMounted = false;
        loading = true;
                    
        try {
            sm.mountObb(obbPath, null, new OnObbStateChangeListener() {
                @Override
                public void onObbStateChange(String path, int state) {
                    isMounted = state == OnObbStateChangeListener.MOUNTED;
                    mountedObbPath = path;
                    loading = false;
                }
            });
        } catch (Exception e) {
            Log.d(TAG, "OBB Mount exception", e);
            loading = false;
        }
    }

    public boolean isLoading() {
        return loading;
    }
    
    public boolean isMounted() {
        return isMounted;
    }
    
    public String getMountedObbPath() {
        return mountedObbPath;
    }  
}