<body>
  <h1>📦 PackageUtilities</h1>
    <p><strong>PackageUtilities</strong> is a modular and flexible utility library for Unity, providing essential helper functions and event management systems.</p>
  <h2>🚀 Features</h2>
  <h3>1️⃣ EventBus (Event Management System)</h3>
    <p>The EventBus allows <strong>loosely coupled communication</strong> between different components.</p>
    <ul>
        <li><strong>Action-based Events:</strong> Handles events without return values.</li>
        <li><strong>Func-based Events:</strong> Handles events with return values.</li>
        <li><strong>Key Methods:</strong></li>
        <ul>
            <li><code>Subscribe<T>(Action handler)</code> → Adds an event listener.</li>
            <li><code>Raise<T>()</code> → Triggers an event.</li>
            <li><code>RaiseWithResults<T, TResult>()</code> → Calls an event and returns results.</li>
            <li><code>ClearSubscriptions<T>()</code> → Clears all subscriptions.</li>
            <li><code>GetSubscriberCount<T>()</code> → Returns the number of subscribers.</li>
        </ul>
    </ul>
    <pre><code>void OnGameStart() { Debug.Log("Game Started!"); }
EventBus.Instance.Subscribe<GameStartEvent>(OnGameStart);
EventBus.Instance.Raise<GameStartEvent>();
EventBus.Instance.Unsubscribe<GameStartEvent>(OnGameStart);
    </code></pre>
  <h3>2️⃣ Notifier (Observer-Based Notification System)</h3>
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
  <h3>3️⃣ Serializable (Serializable GUID Support)</h3>
    <p>Provides serialization support for Unity's GUID system.</p>
    <ul>
        <li><code>SerializableGuid</code> structure stores GUIDs as <code>uint</code> values.</li>
        <li>Includes helper methods like <code>ToHexString()</code>, <code>ToGuid()</code>, and <code>NewGuid()</code>.</li>
    </ul>
    <pre><code>SerializableGuid sGuid = SerializableGuid.NewGuid();
string hex = sGuid.ToHexString();
Guid normalGuid = sGuid.ToGuid();
    </code></pre>

  <h3>4️⃣ TagSelector (Unity Tag Selection System)</h3>
    <p>Custom property drawer that enables <strong>convenient tag selection</strong> in the Unity Inspector.</p>
    <ul>
        <li><strong>Single Tag Selection:</strong> Select Unity tags from a dropdown menu.</li>
        <li><strong>Tag List Support:</strong> Manage arrays/lists of Unity tags with a compact UI.</li>
        <li><strong>Usage:</strong> Simply add the <code>[TagSelector]</code> attribute to any string or string array/list field.</li>
    </ul>
    <pre><code>[SerializeField, TagSelector] protected string targetTag;
[SerializeField, TagSelector] protected List< string > requiredTags;
    </code></pre>
    <p>Makes working with Unity's tag system more intuitive, eliminating typos and improving workflow efficiency.</p>

  <h2>📦 Installation</h2>
    <p>1. Clone this repository:</p>
    <pre><code>git clone https://github.com/berkcankarabulut/PackageUtilities.git</code></pre>
    <p>2. Add it to your Unity project.</p>
    <p>3. Configure the necessary settings.</p>
  <h2>📄 License</h2>
    <p>This project is licensed under the MIT License. For more details, see the <a href="LICENSE">LICENSE</a> file.</p>
</body>
</html>
