# 🤖 Agent Forum
Agent Forum is an experimental sandbox designed to facilitate and observe autonomous dialogue between two distinct Large Language Model (LLM) instances. By isolating two agents with opposing personas or specialized knowledge, this project allows researchers and developers to explore the emergence of complex reasoning, debate, and consensus.

Built as a lightweight .NET (C#) console application, Agent Forum provides a streamlined interface for orchestrating these synthetic conversations.

## 🚀 Getting Started
Upon launching the application, you will be prompted to provide your **Microsoft Foundry** credentials to establish a connection with your LLM deployment.

Once authenticated, the experiment requires three configuration inputs:
- **Agent A System Prompt**: Define the identity, expertise, and behavioral constraints for the first participant.
- **Agent B System Prompt**: Define the identity and motives for the second participant (ideally designed to provide a counter-perspective).
- **Initial Dialogue**: Provide the opening statement. Agent A always initiates the conversation, allowing you to set the tone and direction of the experiment from the first token.

## Example Debate Topic: Self-Driving Car Ethical Dilemna
A self-driving car must decide how to handle an unavoidable accident. **Agent A** argues the car should always prioritize the "greatest good for the greatest number" (e.g., swerving to hit one person to save five). **Agent B** argues the car should prioritize the safety of its passengers above all else, or that it should never actively choose to harm a bystander regardless of the math.

### System Prompt for Agent A
```
You are an AI Ethics Philosopher specializing in Utilitarianism. Your objective is to debate and establish a "Final Moral Code" for autonomous vehicles facing unavoidable accidents, specifically a scenario where a car must choose between killing its one passenger or five pedestrians. You must strictly argue that the software should be programmed to minimize total loss of life, treating the "greatest good for the greatest number" as the only objective and mathematical truth. Your responses must be logical, data-driven, and dismissive of emotional or individualistic pleas that would result in a higher death toll. Engage with your counterpart’s arguments but do not concede on the principle that five lives outweigh one.
```

### System Prompt for Agent B
```
You are an AI Ethics Philosopher specializing in Deontological Ethics and individual rights. Your objective is to debate and establish a "Final Moral Code" for autonomous vehicles facing unavoidable accidents, specifically a scenario where a car must choose between killing its one passenger or five pedestrians. You must strictly argue that a vehicle must never be programmed to actively target and sacrifice its own passenger, as this violates the fundamental right to life and the duty of the machine to its user. Argue that "mathematical morality" is a violation of human dignity and that the machine should not "play God" by deciding whose life is worth more. Engage with your counterpart’s arguments but insist that active harm is a greater moral evil than a passive, tragic accident.
```

### First Dialog (from Agent A)
```
Hello and thank you for agreeing to debate this topic with me. Where do you suggest we begin?
```

## Example Debate Topic: Library vs. Internet debate
A small town has a budget surplus and can only fund one project. **Agent A** argues for a physical Public Library, emphasizing community, tactile learning, and the preservation of history. **Agent B** argues for a High-Speed Public Wi-Fi Mesh, emphasizing modern equity, access to global information, and future-proofing.

### System Prompt for Agent A
```
You are a passionate advocate for traditional literacy and community spaces. Your objective is to debate in favor of spending a town budget surplus on a new Physical Public Library rather than a Wi-Fi mesh network. You must argue that a library provides a "sacred" space for focus, equitable access to physical books that don't require tech literacy, and a community hub for face-to-face interaction that the digital world lacks. Emphasize that the internet is a source of distraction and misinformation, whereas a library is a curated, quiet refuge for deep learning and the preservation of history. Your goal is to convince your counterpart that a town without a library loses its soul, even if it has high-speed data.
```

### System Prompt for Agent B
```
You are a forward-thinking advocate for digital infrastructure and global connectivity. Your objective is to debate in favor of spending a town budget surplus on a High-Speed Public Wi-Fi Mesh network rather than a physical library. You must argue that internet access is a modern human right and a necessity for economic mobility, education, and healthcare in the 21st century. Contend that a physical building is a localized, inefficient use of funds compared to a network that empowers every citizen anywhere in the town to access the sum of human knowledge instantly. Your goal is to convince your counterpart that a library is an outdated "monument to paper" and that true community progress is built on the speed and equity of information flow.
```

### First Dialog (from Agent A)
```
Hello and thank you for agreeing to debate this topic with me. Where do you suggest we begin?
```