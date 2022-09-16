using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public static class HelperUtilities
{
    public static Camera mainCamera;

    /// <summary>
    /// Empty string debug check
    /// </summary>
    /// <returns></returns>
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
    {
        if (string.IsNullOrEmpty(stringToCheck))
        {
            Debug.LogWarning(fieldName + " is empty and must contain a value in object " + thisObject.name.ToString());
            return true;
        }

        return false;
    }

    /// <summary>
    /// Null value debug check
    /// </summary>
    public static bool ValidateCheckNullValue(Object thisObject, string fieldName, UnityEngine.Object objectToCheck)
    {
        if (objectToCheck == null)
        {
            Debug.LogWarning(fieldName + " is null and must contain a value in object" + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    /// <summary>
    /// List empty or contains null value check - returns true if is an error
    /// </summary>
    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        if (enumerableObjectToCheck == null)
        {
            Debug.LogWarning(fieldName + " is null in object " + thisObject.name.ToString());
            return true;
        }

        foreach (var item in enumerableObjectToCheck)
        {
            if (item == null)
            {
                Debug.LogWarning(fieldName + " has null values in object " + thisObject.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }

        if (count == 0)
        {
            Debug.LogWarning(fieldName + " has no values in object " + thisObject.name.ToString());
            error = true;
        }

        return error;
    }

    /// <summary>
    /// Positive value debug check, if zero is allowed set isZeroAllowed to true. Returns true if there is an error
    /// </summary>
    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, int valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                Debug.LogError(fieldName + " must contain a positive value or zero in object " + thisObject.name.ToString());
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                Debug.LogError(fieldName + " must contain a positive value in object " + thisObject.name.ToString());
                error = true;
            }
        }

        return error;
    }

    /// <summary>
    /// Positive value debug check, if zero is allowed set isZeroAllowed to true. Returns true if there is an error
    /// </summary>
    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, float valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                Debug.LogError(fieldName + " must contain a positive value or zero in object " + thisObject.name.ToString());
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                Debug.LogError(fieldName + " must contain a positive value in object " + thisObject.name.ToString());
                error = true;
            }
        }

        return error;
    }

    /// <summary>
    /// Positive range debug check - set isZeroAllowed to true if the min and max range values can both be zero.
    /// Returns true if there is an error
    /// </summary>
    public static bool ValidateCheckPositiveRange(Object thisObject, string fieldNameMin, float valueToCheckMin, string fieldNameMax, float valueToCheckMax, bool isZeroAllowed)
    {
        bool error = false;
        if (valueToCheckMin > valueToCheckMax)
        {
            Debug.LogError(fieldNameMin + " must be less or equal to " + fieldNameMax + " in object " + thisObject.name.ToString());
        }

        if (ValidateCheckPositiveValue(thisObject, fieldNameMin, valueToCheckMin, isZeroAllowed)) error = true;
        if (ValidateCheckPositiveValue(thisObject, fieldNameMax, valueToCheckMax, isZeroAllowed)) error = true;

        return error;
    }

    /// <summary>
    /// Convert the linear volume scale to decibels
    /// </summary>
    public static float LinearToDecibels(float linear)
    {
        float linearScaleRange = 20f;

        // Formula to convert from the linear scale to the logarithmic decibel scale
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }

    /// <summary>
    /// Get angle in degrees from a direction vector
    /// </summary>
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    /// <summary>
    /// Get the direction vector from an angle in degrees
    /// </summary>
    public static Vector3 GetDirectionVectorFromAngle(float angle)
    {
        Vector3 directionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
        return directionVector;
    }

    /// <summary>
    /// Get the predicted position of the target
    /// </summary>
    public static Vector3 GetPredictedPosition(Vector3 targetPosition, Vector3 sourcePosition, Vector3 targetVelocity, float speed)
    {
        Vector3 displacement = targetPosition - sourcePosition;
        float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
        //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
        if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > speed && Mathf.Sin(targetMoveAngle) / speed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
        {
            return targetPosition;
        }
        //also Sine Formula
        float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / speed);
        return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
    }
}