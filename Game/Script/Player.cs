using Godot;

public partial class Player : CharacterBody2D
{
  public const float Speed = 150.0f;
  public const float ACCELERATE = 5.0f;
  public string facingDirection = "Down";
  private AnimatedSprite2D animatedSprite2D;
  private string currentAnimation = "";

  public override void _Ready()
  {
	// 缓存动画节点，找不到则保持空引用
	animatedSprite2D = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
  }

  public override void _PhysicsProcess(double delta)
  {
	// 动画节点不存在时直接退出，避免运行期报错
	if (animatedSprite2D == null)
	  return;

	Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");

	// 插值系数限制在 0~1，避免卡顿时过冲
	float t = Mathf.Clamp((float)(ACCELERATE * delta), 0f, 1f);
	Velocity = Velocity.Lerp(direction * Speed, t);
	MoveAndSlide();
	facingDirection = GetDirection(direction);
	// 只有动画发生变化时才切换，避免重复重启动画
	string nextAnimation = Velocity.Length() > 20 ? "Run_" + facingDirection : "Idle_" + facingDirection;
	if (nextAnimation != currentAnimation)
	{
	  currentAnimation = nextAnimation;
	  animatedSprite2D.Play(currentAnimation);
	}
  }
  public string GetDirection(Vector2 direction)
  {
	if (direction == Vector2.Zero)
	  return facingDirection;

	// 对角线时按主要轴决定朝向
	float absX = Mathf.Abs(direction.X);
	float absY = Mathf.Abs(direction.Y);
	if (absY >= absX)
	  facingDirection = direction.Y > 0 ? "Down" : "Up";
	else
	  facingDirection = direction.X > 0 ? "Right" : "Left";
	return facingDirection;
  }
}
