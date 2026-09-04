using Avalonia;
using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using static MpvNet.Native.LibMpv;

namespace MpvNet.Avalonia.Controls;

public class MpvGlView : OpenGlControlBase
{
	protected unsafe override void OnOpenGlRender(GlInterface gl, int fb)
	{
		if (MpvPlayer?.MpvRenderContext is null or 0) return;
		gl.ClearColor(0, 0, 0, 1);
		gl.Clear(0x00004000);
		var topLevel = TopLevel.GetTopLevel(this);
		var scaling = topLevel?.RenderScaling ?? 1.0;
		var w = (int)(Bounds.Width * scaling);
		var h = (int)(Bounds.Height * scaling);
		var flip_y = 1;
		MpvOpenGLFramebuffer framebuffer = new() { fbo = fb, width = w, height = h };

		MpvRenderParam[] param = {
		new() { type = mpv_render_param_type.MPV_RENDER_PARAM_OPENGL_FBO, data = &framebuffer },
		new() { type = mpv_render_param_type.MPV_RENDER_PARAM_FLIP_Y, data = &flip_y },
		new()
		};
		fixed (MpvRenderParam* p = &param[0])
		{
			if (MpvPlayer.MpvRenderContext == nint.Zero) return;
			mpv_render_context_render(MpvPlayer.MpvRenderContext, p);
		}
		gl.Finish();
	}

	protected override void OnOpenGlInit(GlInterface gl)
	{

		if (MpvPlayer is null) return;
		MpvPlayer.GlGetProcAddress = gl.GetProcAddress;
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

	protected override void OnOpenGlDeinit(GlInterface gl)
	{
		base.OnOpenGlDeinit(gl);
		//todo
	}
}
