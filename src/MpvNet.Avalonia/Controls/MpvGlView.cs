using Avalonia;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using static MpvNet.Native.LibMpv;

namespace MpvNet.Avalonia.Controls;

public class MpvGlView : OpenGlControlBase
{
	protected unsafe override void OnOpenGlRender(GlInterface gl, int fb)
	{
		var size = this.Bounds;
		var w = (int)size.Width;
		var h = (int)size.Height;
		var flip_y = 1;
		MpvOpenGLFramebuffer framebuffer = new()
		{
			fbo = fb,
			width = w,
			height = h,
		};
		MpvRenderParam[] param = {
			  new() {
				type = mpv_render_param_type.MPV_RENDER_PARAM_OPENGL_FBO,
				data = &framebuffer,
			  },
				new() {
					type = mpv_render_param_type.MPV_RENDER_PARAM_FLIP_Y,
					data = &flip_y,
				},
				new()
			};
		fixed (MpvRenderParam* p = &param[0])
		{
			if (MpvPlayer.MpvRenderContext == nint.Zero) return;
			mpv_render_context_render(MpvPlayer.MpvRenderContext, p);
		}
	}

	internal void TriggerRender()
	{
		RequestNextFrameRendering();
	}

	protected override void OnOpenGlInit(GlInterface gl)
	{
		MpvPlayer.GlGetProcAddress = (string name) => gl.GetProcAddress(name);
		MpvPlayer.GlViewDoRender = () => Dispatcher.UIThread.Post(() => RequestNextFrameRendering());
		MpvPlayer.InitGlFrontend();
		base.OnOpenGlInit(gl);
	}

	public static readonly StyledProperty<IGlEnabledPlayer> MpvPlayerProperty = AvaloniaProperty.Register<MpvGlView, IGlEnabledPlayer>(nameof(MpvPlayer));
	public IGlEnabledPlayer MpvPlayer
	{
		get => GetValue(MpvPlayerProperty);
		set
		{
			SetValue(MpvPlayerProperty, value);
		}
	}
}
