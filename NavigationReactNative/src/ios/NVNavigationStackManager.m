#import "NVNavigationStackManager.h"
#import "NVNavigationStackView.h"

@implementation NVNavigationStackManager

RCT_EXPORT_MODULE()

- (UIView *)view
{
    return [[NVNavigationStackView alloc] initWithBridge:self.bridge];
}

RCT_EXPORT_VIEW_PROPERTY(keys, NSArray)
RCT_EXPORT_VIEW_PROPERTY(enterAnim, NSString)
RCT_EXPORT_VIEW_PROPERTY(mostRecentEventCount, NSInteger)
RCT_EXPORT_VIEW_PROPERTY(onWillNavigateBack, RCTDirectEventBlock)
RCT_EXPORT_VIEW_PROPERTY(onRest, RCTDirectEventBlock)

@end
