<template>
  <span>
    <Btn
      :label="finalLabel"
      :id="`uploadBtn${_uid}`"
      :disabled="disabled"
      primary
      @click="$refs.file.click()" />
    <input
      ref="file"
      type="file"
      style="position: fixed; top: -100em"
      @change="pick">
  </span>
</template>

<script>
  import axios from "axios";

  export default {
    name: "UploadButton",
    props: {
      value: {
        type: String,
        required: false,
        default: null,
      },
      prefix: {
        type: [String, Number],
        required: false,
        default: null,
      },
      sendLabel: {
        type: String,
        required: false,
        default: "Enviar",
      },
      sentLabel: {
        type: String,
        required: false,
        default: "OK",
      },
    },
    data() {
      return {
        disabled: false,
      };
    },
    computed: {
      finalLabel() {
        return this.value ? this.sentLabel : this.sendLabel;
      },
    },
    methods: {
      async pick(event) {
        this.disabled = true;

        const file = event.target.files[0];
        await this.$store.dispatch("presign", `${this.prefix}-${file.name}`);
        const url = this.$store.getters.uploadUrl;

        await axios({
          url,
          headers: { "x-ms-blob-type": "BlockBlob" },
          method: "PUT",
          data: file,
        });
        this.$emit("input", url.split("?")[0]);
        this.disabled = false;
      },
    },
  };
</script>
