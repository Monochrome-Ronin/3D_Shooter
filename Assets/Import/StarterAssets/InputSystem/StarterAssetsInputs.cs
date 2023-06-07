using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;
		public bool firstWeapon;
		public bool secondWeapon;
		public bool thirdWeapon;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnFirstWeapon(InputValue value)
		{
			FirstWeaponInput(value.isPressed);
		}

		public void OnSecondWeapon(InputValue value)
		{
			SecondWeaponInput(value.isPressed);
		}

		public void OnThirdWeapon(InputValue value)
		{
			ThirdWeaponInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}

		public void FirstWeaponInput(bool newFirstWeaponState)
		{
			firstWeapon = newFirstWeaponState;
			secondWeapon = false;
			thirdWeapon = false;
		}

		public void SecondWeaponInput(bool newSecondWeaponState)
		{
			secondWeapon = newSecondWeaponState;
			firstWeapon = false;
			thirdWeapon = false;
		}

		public void ThirdWeaponInput(bool newThirdWeaponState)
		{
			thirdWeapon = newThirdWeaponState;
			firstWeapon = false;
			secondWeapon = false;
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}