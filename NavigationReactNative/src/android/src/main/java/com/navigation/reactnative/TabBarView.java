package com.navigation.reactnative;

import android.content.Context;
import android.database.DataSetObserver;
import android.os.Build;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ScrollView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.coordinatorlayout.widget.CoordinatorLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentActivity;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.viewpager.widget.ViewPager;

import com.facebook.react.bridge.Arguments;
import com.facebook.react.bridge.ReactContext;
import com.facebook.react.bridge.WritableMap;
import com.facebook.react.uimanager.events.NativeGestureUtil;
import com.facebook.react.uimanager.events.RCTEventEmitter;

import java.util.ArrayList;
import java.util.List;

public class TabBarView extends ViewPager {
    int selectedTab = 0;
    boolean swipeable = true;
    boolean scrollsToTop;
    private boolean layoutRequested = false;
    int nativeEventCount;
    int mostRecentEventCount;
    private boolean dataSetChanged = false;

    public TabBarView(Context context) {
        super(context);
        addOnPageChangeListener(new TabChangeListener());
        FragmentActivity activity = (FragmentActivity) ((ReactContext) context).getCurrentActivity();
        Adapter adapter = new Adapter(activity.getSupportFragmentManager());
        adapter.registerDataSetObserver(new DataSetObserver() {
            @Override
            public void onChanged() {
                if (getCurrentItem() != selectedTab && getTabsCount() > selectedTab)
                    setCurrentItem(selectedTab, false);
            }
        });
        setAdapter(adapter);
    }

    @Override
    protected void onAttachedToWindow() {
        super.onAttachedToWindow();
        requestLayout();
        if (getTabView() != null)
            getTabView().setupWithViewPager(this);
        populateTabs();
    }

    void populateTabs() {
        TabView tabView = getTabView();
        if (tabView != null && getAdapter() != null) {
            for(int i = 0; i < tabView.getTabCount(); i++) {
                getAdapter().tabFragments.get(i).tabBarItem.setTabView(tabView, i);
            }
        }
    }

    private TabView getTabView() {
        ViewGroup parent = (ViewGroup) getParent();
        if (parent instanceof CoordinatorLayout) {
            parent = (ViewGroup) parent.getChildAt(0);
        }
        for(int i = 0; parent != null && i < parent.getChildCount(); i++) {
            View child = parent.getChildAt(i);
            if (child instanceof TabView)
                return (TabView) child;
        }
        return null;
    }

    void scrollToTop() {
        if (!scrollsToTop)
            return;
        View tabBarItem = getTabAt(getCurrentItem());
        if (tabBarItem instanceof ViewGroup) {
            ViewGroup viewGroup = (ViewGroup) tabBarItem;
            for(int i = 0; i < viewGroup.getChildCount(); i++) {
                if (viewGroup.getChildAt(i) instanceof NavigationBarView)
                    ((NavigationBarView) viewGroup.getChildAt(i)).setExpanded(true);
                if (viewGroup.getChildAt(i) instanceof ScrollView)
                    ((ScrollView) viewGroup.getChildAt(i)).smoothScrollTo(0,0);
                if (viewGroup.getChildAt(i) instanceof TabBarView)
                    ((TabBarView) viewGroup.getChildAt(i)).scrollToTop();
            }
        }
        if (tabBarItem instanceof ScrollView)
            ((ScrollView) tabBarItem).smoothScrollTo(0, 0);
    }

    @Override
    public void requestLayout() {
        super.requestLayout();
        if (!layoutRequested) {
            layoutRequested = true;
            post(measureAndLayout);
        }
    }

    private final Runnable measureAndLayout = new Runnable() {
        @Override
        public void run() {
            layoutRequested = false;
            measure(
                MeasureSpec.makeMeasureSpec(getWidth(), MeasureSpec.EXACTLY),
                MeasureSpec.makeMeasureSpec(getHeight(), MeasureSpec.EXACTLY));
            layout(getLeft(), getTop(), getRight(), getBottom());
        }
    };

    @Nullable
    @Override
    public Adapter getAdapter() {
        return (Adapter) super.getAdapter();
    }

    int getTabsCount() {
        return getAdapter() != null ? getAdapter().tabFragments.size() : 0;
    }

    View getTabAt(int index) {
        return getAdapter() != null ? getAdapter().tabFragments.get(index).tabBarItem.content.get(0) : null;
    }

    void addTab(TabBarItemView tab, int index) {
        if (getAdapter() != null) {
            getAdapter().addTab(tab, index);
            populateTabs();
        }
    }

    void removeTab(int index) {
        if (getAdapter() != null) {
            getAdapter().removeTab(index);
            populateTabs();
        }
    }

    @Override
    public boolean onInterceptTouchEvent(MotionEvent ev) {
        try {
            if (swipeable && super.onInterceptTouchEvent(ev)) {
                NativeGestureUtil.notifyNativeGestureStarted(this, ev);
                return true;
            }
        } catch (IllegalArgumentException ignored) {
        }
        return false;
    }

    @Override
    public boolean onTouchEvent(MotionEvent ev) {
        try {
            return swipeable && super.onTouchEvent(ev);
        } catch (IllegalArgumentException ignored) {
        }

        return false;
    }

    private class Adapter extends FragmentPagerAdapter {
        private List<TabFragment> tabFragments = new ArrayList<>();

        Adapter(FragmentManager fragmentManager) {
            super(fragmentManager, BEHAVIOR_RESUME_ONLY_CURRENT_FRAGMENT);
        }

        void addTab(TabBarItemView tab, int index) {
            tabFragments.add(index, new TabFragment(tab));
            dataSetChanged = true;
            notifyDataSetChanged();
            dataSetChanged = false;
        }

        void removeTab(int index) {
            tabFragments.remove(index);
            dataSetChanged = true;
            notifyDataSetChanged();
            dataSetChanged = false;
        }

        @Override
        public int getCount() {
            return tabFragments.size();
        }

        @NonNull
        @Override
        public Fragment getItem(int position) {
            return tabFragments.get(position);
        }

        @Nullable
        @Override
        public CharSequence getPageTitle(int position) {
            return tabFragments.get(position).tabBarItem.title;
        }

        @Override
        public int getItemPosition(@NonNull Object object) {
            for(int i = 0; i < tabFragments.size(); i++) {
                if (tabFragments.get(i) == object)
                    return i;
            }
            return POSITION_NONE;
        }

        @Override
        public long getItemId(int position) {
            return tabFragments.get(position).hashCode();
        }
    }

    public static class TabFragment extends Fragment {
        TabBarItemView tabBarItem;
        View view;

        TabFragment(TabBarItemView tabBarItem) {
            super();
            this.tabBarItem = tabBarItem;
            view = tabBarItem.content.get(0);
            if (view instanceof NavigationStackView)
                ((NavigationStackView) view).onAfterUpdateTransaction();
        }

        @Nullable
        @Override
        public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
            return view;
        }
    }

    private class TabChangeListener implements ViewPager.OnPageChangeListener {

        @Override
        public void onPageScrolled(int position, float positionOffset, int positionOffsetPixels) {
        }

        @Override
        public void onPageSelected(int position) {
            if (!dataSetChanged)
                nativeEventCount++;
            selectedTab = position;
            WritableMap event = Arguments.createMap();
            event.putInt("tab", position);
            event.putInt("eventCount", nativeEventCount);
            ReactContext reactContext = (ReactContext) getContext();
            reactContext.getJSModule(RCTEventEmitter.class).receiveEvent(getId(),"onTabSelected", event);
            if (getAdapter() != null)
                getAdapter().tabFragments.get(position).tabBarItem.pressed();
        }

        @Override
        public void onPageScrollStateChanged(int state) {
        }
    }
}
