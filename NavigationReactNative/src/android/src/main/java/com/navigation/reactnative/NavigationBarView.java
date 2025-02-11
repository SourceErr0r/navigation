package com.navigation.reactnative;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.view.ViewOutlineProvider;

import androidx.core.util.Pools;
import androidx.core.view.ViewCompat;

import com.facebook.react.bridge.Arguments;
import com.facebook.react.bridge.ReactContext;
import com.facebook.react.bridge.WritableMap;
import com.facebook.react.modules.i18nmanager.I18nUtil;
import com.facebook.react.uimanager.PixelUtil;
import com.facebook.react.uimanager.UIManagerModule;
import com.facebook.react.uimanager.events.Event;
import com.facebook.react.uimanager.events.RCTEventEmitter;
import com.google.android.material.appbar.AppBarLayout;

public class NavigationBarView extends AppBarLayout {
    final ViewOutlineProvider defaultOutlineProvider;
    final Drawable defaultBackground;

    public NavigationBarView(Context context) {
        super(context);
        ViewCompat.setLayoutDirection(this, !I18nUtil.getInstance().isRTL(context) ? ViewCompat.LAYOUT_DIRECTION_LTR : ViewCompat.LAYOUT_DIRECTION_RTL);
        setLayoutParams(new AppBarLayout.LayoutParams(AppBarLayout.LayoutParams.MATCH_PARENT, AppBarLayout.LayoutParams.WRAP_CONTENT));
        defaultOutlineProvider = getOutlineProvider();
        defaultBackground = getBackground();
        addOnOffsetChangedListener(new OnOffsetChangedListener() {
            @Override
            public void onOffsetChanged(AppBarLayout appBarLayout, int offset) {
                OffsetChangedEvent event = OffsetChangedEvent.obtain(getId(), offset);
                ReactContext reactContext = (ReactContext) getContext();
                reactContext.getNativeModule(UIManagerModule.class).getEventDispatcher().dispatchEvent(event);
            }
        });
    }

    static class OffsetChangedEvent extends Event<OffsetChangedEvent> {
        private int offset;
        private static final Pools.SynchronizedPool<OffsetChangedEvent> pool = new Pools.SynchronizedPool<>(3);

        private OffsetChangedEvent() {
        }

        private static OffsetChangedEvent obtain(int viewTag, int offset) {
            OffsetChangedEvent event = pool.acquire();
            if (event == null)
                event = new OffsetChangedEvent();
            event.init(viewTag);
            event.offset = offset;
            return event;
        }

        @Override
        public void onDispose() {
            pool.release(this);
        }

        @Override
        public short getCoalescingKey() {
            return 0;
        }

        @Override
        public boolean canCoalesce() {
            return true;
        }

        @Override
        public String getEventName() {
            return "onOffsetChanged";
        }

        @Override
        public void dispatch(RCTEventEmitter rctEventEmitter) {
            WritableMap event = Arguments.createMap();
            event.putDouble("offset", PixelUtil.toDIPFromPixel(offset));
            rctEventEmitter.receiveEvent(getViewTag(), getEventName(), event);
        }
    }
}
