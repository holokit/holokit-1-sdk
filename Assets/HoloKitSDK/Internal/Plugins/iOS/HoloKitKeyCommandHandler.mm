//
//  HoloKitKeyCommandHandler.m
//  Unity-iPhone
//
//  Created by Yang Liu on 6/18/17.
//

#import <Foundation/Foundation.h>

@interface HoloKitKeyCommandHandlerView : UIView
@end

@implementation HoloKitKeyCommandHandlerView

static const NSString *keysToListen = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789[]\\,./`-=";

- (id)initWithFrame:(CGRect)frame {
    self = [super initWithFrame:frame];
    if (self) {
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(didEnterBackground) name:UIApplicationDidEnterBackgroundNotification object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(didBecomeActive) name:UIApplicationDidBecomeActiveNotification object:nil];
        
        [self becomeFirstResponder];
        
        NSLog(@"HoloKitKeyCommandHandler: Init");
    }
    
    return self;
}

- (NSArray<UIKeyCommand *> *)keyCommands {
    NSMutableArray *array = [[NSMutableArray alloc] initWithCapacity:keysToListen.length];
    for (int i = 0; i < keysToListen.length; i++)
        [array addObject:[UIKeyCommand keyCommandWithInput:[keysToListen substringWithRange:NSMakeRange(i, 1)]
                                             modifierFlags:0
                                                    action:@selector(onKeyCommand:)]];
    return array;
}

- (void)onKeyCommand:(UIKeyCommand *)sender {
    NSString *selected = sender.input;
    UnitySendMessage("(singleton) HoloKit.RemoteKeyboardReceiver", "OnKeyCommand", [selected cStringUsingEncoding:NSASCIIStringEncoding]);
}

- (BOOL)canBecomeFirstResponder {
    return YES;
}

- (void)didEnterBackground {
        [self resignFirstResponder];
}

- (void)didBecomeActive {
    [self becomeFirstResponder];
    NSLog(@"HoloKitKeyCommandHandler: becomeFirstResponder");
}

@end

extern "C" {
    void InstallKeyCommandHandler() {
        HoloKitKeyCommandHandlerView *view = [[HoloKitKeyCommandHandlerView alloc] initWithFrame:CGRectZero];
        [[UIApplication sharedApplication].keyWindow.rootViewController.view addSubview:view];
    }
}
