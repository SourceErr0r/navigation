package com.navigation.reactnative;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;

import androidx.coordinatorlayout.widget.CoordinatorLayout;

import com.facebook.react.uimanager.PixelUtil;
import com.google.android.material.bottomsheet.BottomSheetBehavior;

public class BottomSheetView extends ViewGroup {
    public BottomSheetView(Context context) {
        super(context);
        CoordinatorLayout.LayoutParams params = new CoordinatorLayout.LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
        params.setBehavior(new BottomSheetBehavior());
        setLayoutParams(params);
    }

    @Override
    protected void onAttachedToWindow() {
        super.onAttachedToWindow();
        BottomSheetBehavior.from(this).setPeekHeight((int) PixelUtil.toPixelFromDIP(50));
    }

    @Override
    protected void onLayout(boolean b, int i, int i1, int i2, int i3) {
    }
}
