using UnityEngine;
using UnityEditor;

namespace OceanSystem
{
	public class OceanShaderEditor : ShaderGUI
	{
		Material targetMaterial;
		MaterialEditor editor;

		// toggles
		MaterialProperty wavesFoamEnabled = null;
		MaterialProperty contactFoamEnabled = null;

		// colors
		MaterialProperty deepScatterColor = null;
		MaterialProperty fogDensity = null;
		MaterialProperty sssColor = null;
		MaterialProperty diffuseColor = null;
		MaterialProperty absorbtionDepthScale = null;

		// downward reflections mask
		MaterialProperty downwardReflectionsColor;
		MaterialProperty downwardReflectionsRadius;
		MaterialProperty downwardReflectionsSharpness;

		// specular
		MaterialProperty specularStrength = null;
		MaterialProperty specularMinRoughness = null;
		MaterialProperty receiveShadows = null;

		// horizon
		MaterialProperty horizonEditorExpanded = null;
		MaterialProperty roughnessScale = null;
		MaterialProperty roughnessDistance = null;
		MaterialProperty horizonFog = null;
		MaterialProperty cascadesFadeDist = null;

		// planar reflections
		MaterialProperty reflectionNormalStength = null;

		// refractions
		MaterialProperty refractionStrength = null;
		MaterialProperty refractionStrengthUnderwater = null;

		// subsurface scattering
		MaterialProperty sssEditorExpanded = null;
		MaterialProperty sssSunStrength = null;
		MaterialProperty sssEnvironmentStrength = null;
		MaterialProperty sssSpread = null;
		MaterialProperty sssNormalStrength = null;
		MaterialProperty sssHeightBias = null;
		MaterialProperty sssFadeDistance = null;

		// foam
		MaterialProperty foamEditorExpanded = null;
		MaterialProperty foamAlbedo = null;
		MaterialProperty foamUnderwaterTexture = null;
		MaterialProperty foamTrailTexture = null;
		MaterialProperty contactFoamTexture = null;
		MaterialProperty foamNormalsDetail = null;
		MaterialProperty surfaceFoamTint = null;
		MaterialProperty underwaterFoamParallax = null;
		MaterialProperty contactFoam = null;

		Material skyMapMaterial;

		public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
		{
			targetMaterial = editor.target as Material;
			this.editor = editor;
			skyMapMaterial = new Material(Shader.Find("Hidden/Ocean/StereographicSky"));
			FindProperties(properties);
			ShaderProperties();
		}

        void FindProperties(MaterialProperty[] properties)
		{
			// keywords
			wavesFoamEnabled = FindProperty("_WAVES_FOAM_ENABLED", properties);
			contactFoamEnabled = FindProperty("_CONTACT_FOAM_ENABLED", properties);

			// colors
			deepScatterColor = FindProperty("_DeepScatterColor", properties);
			fogDensity = FindProperty("_FogDensity", properties);
			sssColor = FindProperty("_SssColor", properties);
			diffuseColor = FindProperty("_DiffuseColor", properties);
			absorbtionDepthScale = FindProperty("_AbsorptionDepthScale", properties);

			// downward reflections mask
			downwardReflectionsColor = FindProperty("_DownwardReflectionsColor", properties);
			downwardReflectionsRadius = FindProperty("_DownwardReflectionsRadius", properties); ;
			downwardReflectionsSharpness = FindProperty("_DownwardReflectionsSharpness", properties); ;

			// specular
			specularStrength = FindProperty("_SpecularStrength", properties);
			specularMinRoughness = FindProperty("_SpecularMinRoughness", properties);
			receiveShadows = FindProperty("_ReceiveShadows", properties);

			// horizon
			horizonEditorExpanded = FindProperty("horizonEditorExpanded", properties);
			roughnessScale = FindProperty("_RoughnessScale", properties);
			roughnessDistance = FindProperty("_RoughnessDistance", properties);
			horizonFog = FindProperty("_HorizonFog", properties);
			cascadesFadeDist = FindProperty("_CascadesFadeDist", properties);

			// planar reflections
			reflectionNormalStength = FindProperty("_ReflectionNormalStength", properties);

			// refractions
			refractionStrength = FindProperty("_RefractionStrength", properties);
			refractionStrengthUnderwater = FindProperty("_RefractionStrengthUnderwater", properties);

			// subsurface scattering
			sssEditorExpanded = FindProperty("sssEditorExpanded", properties);
			sssSunStrength = FindProperty("_SssSunStrength", properties);
			sssEnvironmentStrength = FindProperty("_SssEnvironmentStrength", properties);
			sssSpread = FindProperty("_SssSpread", properties);
			sssNormalStrength = FindProperty("_SssNormalStrength", properties);
			sssHeightBias = FindProperty("_SssHeightBias", properties);
			sssFadeDistance = FindProperty("_SssFadeDistance", properties);

			// foam
			foamEditorExpanded = FindProperty("foamEditorExpanded", properties);
			foamNormalsDetail = FindProperty("_FoamNormalsDetail", properties);
			surfaceFoamTint = FindProperty("_FoamTint", properties);
			foamAlbedo = FindProperty("_FoamAlbedo", properties);
			foamUnderwaterTexture = FindProperty("_FoamUnderwaterTexture", properties);
			foamTrailTexture = FindProperty("_FoamTrailTexture", properties);
			underwaterFoamParallax = FindProperty("_UnderwaterFoamParallax", properties);
			contactFoam = FindProperty("_ContactFoam", properties);
			contactFoamTexture = FindProperty("_ContactFoamTexture", properties);
		}

		void ShaderProperties()
		{
			EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel);
			EditorGUI.indentLevel += 1;
			editor.ShaderProperty(deepScatterColor, MakeLabel(deepScatterColor));
			editor.ShaderProperty(fogDensity, MakeLabel(fogDensity));
			editor.ShaderProperty(sssColor, MakeLabel(sssColor));
			editor.ShaderProperty(diffuseColor, MakeLabel(diffuseColor));
			editor.ShaderProperty(downwardReflectionsColor, MakeLabel(downwardReflectionsColor));
			EditorGUI.BeginChangeCheck();
			Gradient tint = EditorGUILayout.GradientField(new GUIContent("Absorbtion Gradient"), GetGradient(OceanMaterialProps.TintGradient), false);
			if (EditorGUI.EndChangeCheck())
				SetGradient(OceanMaterialProps.TintGradient, tint);
			editor.ShaderProperty(absorbtionDepthScale, MakeLabel(absorbtionDepthScale));
			EditorGUI.indentLevel -= 1;
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Downward Reflections Mask", EditorStyles.boldLabel);
			EditorGUI.indentLevel += 1;
			editor.ShaderProperty(downwardReflectionsRadius, MakeLabel(downwardReflectionsRadius));
			editor.ShaderProperty(downwardReflectionsSharpness, MakeLabel(downwardReflectionsSharpness));
			DrawSkyMapPreview();
			Shader.SetGlobalVector(OceanGlobalProps.DownwardReflectionsColorID, targetMaterial.GetVector(OceanMaterialProps.DownwardReflectionsColor));
			Shader.SetGlobalFloat(OceanGlobalProps.DownwardReflectionsRadiusID, targetMaterial.GetFloat(OceanMaterialProps.DownwardReflectionsRadius));
			Shader.SetGlobalFloat(OceanGlobalProps.DownwardReflectionsSharpnessID, targetMaterial.GetFloat(OceanMaterialProps.DownwardReflectionsSharpness));
			EditorGUI.indentLevel -= 1;
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Specular", EditorStyles.boldLabel);
			EditorGUI.indentLevel += 1;
			editor.ShaderProperty(receiveShadows, MakeLabel(receiveShadows));
			if (targetMaterial.GetFloat("_ReceiveShadows") > 0)
				targetMaterial.DisableKeyword("_RECEIVE_SHADOWS_OFF");
			else
				targetMaterial.EnableKeyword("_RECEIVE_SHADOWS_OFF");
			editor.ShaderProperty(specularStrength, MakeLabel(specularStrength));
			editor.ShaderProperty(specularMinRoughness, MakeLabel(specularMinRoughness));
			EditorGUI.indentLevel -= 1;
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Planar Reflections", EditorStyles.boldLabel);
			EditorGUI.indentLevel += 1;
			editor.ShaderProperty(reflectionNormalStength, MakeLabel(reflectionNormalStength));
			EditorGUI.indentLevel -= 1;
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Refraction", EditorStyles.boldLabel);
			EditorGUI.indentLevel += 1;
			editor.ShaderProperty(refractionStrength, MakeLabel(refractionStrength));
			editor.ShaderProperty(refractionStrengthUnderwater, MakeLabel(refractionStrengthUnderwater));
			EditorGUI.indentLevel -= 1;
			EditorGUILayout.Space();

			horizonEditorExpanded.floatValue = EditorGUILayout.BeginFoldoutHeaderGroup(horizonEditorExpanded.floatValue > 0, "Horizon") ? 1 : 0;
			if (horizonEditorExpanded.floatValue > 0)
			{
				EditorGUI.indentLevel += 1;
				editor.ShaderProperty(roughnessScale, MakeLabel(roughnessScale));
				editor.ShaderProperty(roughnessDistance, MakeLabel(roughnessDistance));
				editor.ShaderProperty(horizonFog, MakeLabel(horizonFog));
				editor.ShaderProperty(cascadesFadeDist, MakeLabel(cascadesFadeDist));
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space();
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			sssEditorExpanded.floatValue = EditorGUILayout.BeginFoldoutHeaderGroup(sssEditorExpanded.floatValue > 0, "Subsurface Scattering") ? 1 : 0;
			if (sssEditorExpanded.floatValue > 0)
			{
				EditorGUI.indentLevel += 1;
				editor.ShaderProperty(sssSunStrength, MakeLabel(sssSunStrength));
				editor.ShaderProperty(sssEnvironmentStrength, MakeLabel(sssEnvironmentStrength));
				editor.ShaderProperty(sssSpread, MakeLabel(sssSpread));
				editor.ShaderProperty(sssNormalStrength, MakeLabel(sssNormalStrength));
				editor.ShaderProperty(sssHeightBias, MakeLabel(sssHeightBias));
				editor.ShaderProperty(sssFadeDistance, MakeLabel(sssFadeDistance));
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space();
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			foamEditorExpanded.floatValue = EditorGUILayout.BeginFoldoutHeaderGroup(foamEditorExpanded.floatValue > 0, "Foam") ? 1 : 0;
			if (foamEditorExpanded.floatValue > 0)
			{
				EditorGUI.indentLevel += 1;
				editor.ShaderProperty(wavesFoamEnabled, MakeLabel(wavesFoamEnabled));
				editor.ShaderProperty(contactFoamEnabled, MakeLabel(contactFoamEnabled));
				if (contactFoamEnabled.floatValue > 0)
				{
					EditorGUILayout.HelpBox("Depth Texture must be enabled in the pipline asset for contact foam to work correctly.", MessageType.Info);
					EditorGUILayout.Space();
				}
				editor.TexturePropertySingleLine(new GUIContent("Albedo"), foamAlbedo, surfaceFoamTint);
				DrawTilingProperty(foamAlbedo);

				editor.TexturePropertySingleLine(MakeLabel(foamUnderwaterTexture), foamUnderwaterTexture);
				DrawTilingProperty(foamUnderwaterTexture);

				editor.TexturePropertySingleLine(new GUIContent("Contact Foam"), contactFoamTexture, contactFoam);
				DrawTilingProperty(contactFoamTexture);

				editor.TexturePropertySingleLine(MakeLabel(foamTrailTexture), foamTrailTexture);

				editor.ShaderProperty(foamNormalsDetail, MakeLabel(foamNormalsDetail));
				editor.ShaderProperty(underwaterFoamParallax, MakeLabel(underwaterFoamParallax));
				EditorGUI.indentLevel -= 1;
			}
			EditorGUILayout.EndFoldoutHeaderGroup();
		}

		private static void DrawTilingProperty(MaterialProperty prop)
        {
			Rect rect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(EditorGUIUtility.singleLineHeight * 1.3f));
			float labelWidth = EditorGUIUtility.labelWidth;
			float controlStartX = rect.x + labelWidth;
			Rect labelRect = new Rect(rect.x + 14, rect.y, labelWidth - 14, EditorGUIUtility.singleLineHeight);
			Rect valueRect = new Rect(controlStartX - 14, rect.y, rect.width - labelWidth + 14, EditorGUIUtility.singleLineHeight);
			EditorGUI.PrefixLabel(labelRect, new GUIContent("Tiling"));
			prop.textureScaleAndOffset = EditorGUI.Vector2Field(valueRect, GUIContent.none, prop.textureScaleAndOffset);
		}

		private void DrawSkyMapPreview()
        {
			Rect skyPreviewRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(50));
			Rect skyPreviewLabelRect = skyPreviewRect;
			skyPreviewLabelRect.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.LabelField(skyPreviewLabelRect, "Preview");
			skyPreviewRect.x += EditorGUIUtility.labelWidth;
			skyPreviewRect.width = skyPreviewRect.height;
			EditorGUI.DrawPreviewTexture(skyPreviewRect, EditorGUIUtility.whiteTexture, skyMapMaterial);
		}

		private Gradient GetGradient(int[] propIDs)
        {
			Gradient grad = new Gradient();
			Vector2 pars = targetMaterial.GetVector(propIDs[0]);
			grad.mode = (GradientMode)pars.y;
			int count = (int)pars.x;
			var keys = new GradientColorKey[count];
            for (int i = 0; i < count; i++)
            {
				Vector4 value = targetMaterial.GetVector(propIDs[i + 1]);
				keys[i].color = new Color(Mathf.LinearToGammaSpace(value.x),
					Mathf.LinearToGammaSpace(value.y),
					Mathf.LinearToGammaSpace(value.z));
				keys[i].time = value.w;
			}
			grad.colorKeys = keys;
			return grad;
		}

		private void SetGradient(int[] propIDs, Gradient grad)
		{
			for (int i = 0; i < grad.colorKeys.Length; i++)
			{
				Vector4 v = grad.colorKeys[i].color.linear;
				v.w = grad.colorKeys[i].time;
				targetMaterial.SetVector(propIDs[i + 1], v);
			}
			targetMaterial.SetVector(propIDs[0],
				new Vector2(grad.colorKeys.Length, (float)grad.mode));
		}

		static GUIContent staticLabel = new GUIContent();
		static GUIContent MakeLabel(MaterialProperty property, string tooltip = null)
		{
			staticLabel.text = property.displayName;
			staticLabel.tooltip = tooltip;
			return staticLabel;
		}
	}
}
