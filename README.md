# BugReport.ButtonVisualState
The demostration of one of MAUI's bug associated to Button's VisualState.

Just simply press the two buttons and see the difference.
The correct behavior can be achieved by the using a Clicked event handler, but it's definitely a bug, not a feature.
Depending on this workaround is ugly and unnecessary.

# Major effective codes
## Styles.xaml
(at `Resources\Styles\Styles.xaml`)
```xml
<Style TargetType="Button">
        <Setter Property="TextColor" Value="{AppThemeBinding Light={StaticResource White}, Dark={StaticResource Primary}}" />
        <Setter Property="BackgroundColor" Value="{AppThemeBinding Light={StaticResource Primary}, Dark={StaticResource White}}" />
        <Setter Property="FontFamily" Value="OpenSansRegular"/>
        <Setter Property="FontSize" Value="14"/>
        <Setter Property="CornerRadius" Value="8"/>
        <Setter Property="Padding" Value="14,10"/>
        <Setter Property="VisualStateManager.VisualStateGroups">
            <VisualStateGroupList>
                <VisualStateGroup x:Name="CommonStates">
                    <VisualState x:Name="Normal" />
                    <VisualState x:Name="Disabled">
                        <VisualState.Setters>
                            <Setter Property="TextColor" Value="{AppThemeBinding Light={StaticResource Gray950}, Dark={StaticResource Gray200}}" />
                            <Setter Property="BackgroundColor" Value="{AppThemeBinding Light={StaticResource Gray200}, Dark={StaticResource Gray600}}" />
                        </VisualState.Setters>
                    </VisualState>

                    <VisualState x:Name="Pressed">
                        <VisualState.Setters>
                            <Setter Property="BackgroundColor" Value="{StaticResource Tertiary}" />
                        </VisualState.Setters>
                    </VisualState>
                </VisualStateGroup>
            </VisualStateGroupList>
        </Setter>
    </Style>
```
I added the `Pressed` VisualState of Button, making its BackgroundColor set to `{StaticResource Tertiary}` when pressed.

## MainPage.xaml
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="BugReport.ButtonVisualState.MainPage">
    <Grid Margin="12" 
          HorizontalOptions="CenterAndExpand" 
          VerticalOptions="CenterAndExpand" 
          ColumnDefinitions="*, *"
          ColumnSpacing="80">
        <VerticalStackLayout Grid.Column="0" Spacing="20">
            <Label Text="What I expect:" FontSize="Large" />
            <Button Text="Button VisualState set to `Pressed` when pressed, and reset to `Normal` when released."
                    Clicked="ResetButtonVisualState"/>
        </VerticalStackLayout>

        <VerticalStackLayout Grid.Column="1" Spacing="20">
            <Label Text="What Buttons act now:" FontSize="Large"/>
            <Button Text="Button VisualState set to `Pressed` when pressed, and will not reset automatically."/>
        </VerticalStackLayout>
    </Grid>
</ContentPage>
```
Two buttons are defined, one of which has a `Clicked` event handler in `MainPage.xaml.cs`.

## MainPage.xaml.cs
```csharp
namespace BugReport.ButtonVisualState;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void ResetButtonVisualState(object sender, EventArgs e)
	{
		// Must call `Unfocus` to reset Button's VisualState to `Normal`.
		(sender as Button).Unfocus();
	}
}
```

The event handler is called `ResetButtonVisualState`, whose functionality is simply call the `Unfocus` method of Button instances. This can set the VisualState of Buttons to `Normal`, but it's still a small but annoying bug, not a feature. This bug reveals when targeting Windows, but cannot be reproduced in Android platform. Mac Catalyst and iOS are not tested yet because I don't have Apple devices.
