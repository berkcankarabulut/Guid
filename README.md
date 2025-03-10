<h1>🎮 Scene Loader System</h1>

<p>This project provides a streamlined solution for loading scenes in Unity. With the <strong>SceneLoader</strong> component, you can asynchronously load scenes and manage settings easily.</p>

<h2>🚀 Features</h2>
    <ul>
        <li>🎯 <strong>Editor Integration:</strong> Select and load scenes directly from the Unity Inspector.</li>
        <li>⏳ <strong>Asynchronous Loading:</strong> Load scenes without interrupting gameplay.</li>
        <li>🛠️ <strong>Custom Settings:</strong> Supports both <code>Additive</code> and <code>Single</code> <code>LoadSceneMode</code>.</li>
        <li>🔄 <strong>Auto Load on Awake:</strong> Optionally load scenes automatically on start.</li>
        <li>📡 <strong>Callbacks:</strong> Events triggered when scene loading starts and completes.</li>
    </ul>

  <h2>📦 Installation</h2>
    <p>1. <strong>Clone this repository:</strong></p>
    <pre><code>git clone https://github.com/YOUR_USERNAME/SceneLoaderSystem.git</code></pre>
    <p>2. <strong>Add it to your Unity project.</strong></p>
    <p>3. <strong>Attach the <code>SceneLoader</code> component to a GameObject.</strong></p>
    <p>4. <strong>Configure settings via the Unity Inspector.</strong></p>

  <h2>📜 Usage</h2>
    <h3>🎬 Load a Scene Manually</h3>
    <pre><code>SceneLoader sceneLoader = FindObjectOfType&lt;SceneLoader&gt;();
sceneLoader.LoadScene();</code></pre>

  <h3>🛠️ Load Scenes via the Editor</h3>
    <ol>
        <li><strong>Select the <code>SceneLoader</code> component in the Inspector.</strong></li>
        <li><strong>Choose a scene from the dropdown menu.</strong></li>
        <li><strong>Click the "Load Scene" button to load it.</strong></li>
    </ol>

  <h2>📄 License</h2>
    <p>This project is licensed under the MIT License. See the <a href="LICENSE">LICENSE</a> file for details.</p>

</body>
</html>
