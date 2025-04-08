<body>
  <h1>📦 Utilities</h1>
    <p><strong>Utilities</strong> is a modular and flexible utility library for Unity, providing essential helper functions and event management systems.</p>
  <h2>🚀 Features</h2>
  <h3>1️⃣ Notifier (Observer-Based Notification System)</h3>
    <p>Lightweight messaging system for <strong>independent component communication.</strong></p>
    <ul>
        <li><code>Subscribe(T handler)</code> → Adds an event listener.</li>
        <li><code>Unsubscribe(T handler)</code> → Removes an event listener.</li>
        <li><code>Notify(T data)</code> → Triggers an event.</li>
    </ul>
    <pre><code>Notifier<string> messageNotifier = new Notifier<string>();
messageNotifier.Subscribe(msg => Debug.Log("Received: " + msg));
messageNotifier.Notify("Hello, World!");
    </code></pre>
  <h3>2️⃣ Serializable (Serializable GUID Support)</h3>
    <p>Provides serialization support for Unity's GUID system.</p>
    <ul>
        <li><code>SerializableGuid</code> structure stores GUIDs as <code>uint</code> values.</li>
        <li>Includes helper methods like <code>ToHexString()</code>, <code>ToGuid()</code>, and <code>NewGuid()</code>.</li>
    </ul>
    <pre><code>SerializableGuid sGuid = SerializableGuid.NewGuid();
string hex = sGuid.ToHexString();
Guid normalGuid = sGuid.ToGuid();
    </code></pre>

  <h2>📦 Installation</h2>
    <p>1. Clone this repository:</p>
    <pre><code>git clone https://github.com/berkcankarabulut/PackageUtilities.git</code></pre>
    <p>2. Add it to your Unity project.</p>
    <p>3. Configure the necessary settings.</p>
  <h2>📄 License</h2>
    <p>This project is licensed under the MIT License. For more details, see the <a href="LICENSE">LICENSE</a> file.</p>
</body>
</html>
