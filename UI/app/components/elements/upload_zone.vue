<template>
  <div>
    <input
      ref="file"
      type="file"
      style="position: fixed; top: -100em"
      @change="pick">
    <div
      class="pointer"
      @click="!disabled && $refs.file.click()">
      <slot />
    </div>
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: "UploadZone",
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
      disabled: {
        type: Boolean,
        default: false,
      },
    },
    methods: {
      async pick() {
        const file = this.$refs.file.files[0];
        await this.$store.dispatch("presign", `${this.prefix}${file.name}`);
        const url = this.$store.getters.uploadUrl;

        const headers = { "x-ms-blob-type": "BlockBlob" };
        await axios.put(url, file, { headers });
        this.$emit("input", url.split("?")[0]);
      },
    },
  };
</script>
