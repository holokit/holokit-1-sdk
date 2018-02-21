#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

#ifdef __cplusplus
extern "C" {
#endif

void nativeInterface_setBrightness(float val) {
    [UIScreen mainScreen].brightness = val;
    
}

float nativeInterface_getBrightness() {
    return [UIScreen mainScreen].brightness;
}

#ifdef __cplusplus
}
#endif
